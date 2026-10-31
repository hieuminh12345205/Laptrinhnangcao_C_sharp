using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace QLTVDAO;

// Dùng chung DbContext với các câu lệnh cần lưu trong cùng một giao dịch.
public sealed class DatabaseTransaction : IDisposable
{
    private readonly IDbContextTransaction transaction;
    private bool daKetThuc;

    public DatabaseTransaction(BookDbContext db, IsolationLevel mucCoLap = IsolationLevel.ReadCommitted)
    {
        ArgumentNullException.ThrowIfNull(db);
        transaction = db.Database.BeginTransaction(mucCoLap);
    }

    public void Commit()
    {
        if (daKetThuc) throw new InvalidOperationException("Giao dịch đã kết thúc.");
        transaction.Commit(); // Chấp nhận tất cả lệnh đã SaveChanges trong giao dịch.
        daKetThuc = true;
    }

    public void Rollback()
    {
        if (daKetThuc) return;
        transaction.Rollback(); // Hoàn tác tất cả lệnh đã ghi trong giao dịch.
        daKetThuc = true;
    }

    public void Dispose()
    {
        // EF/MySQL tự hoàn tác giao dịch chưa Commit khi giải phóng transaction.
        // Không Dispose DbContext vì đối tượng đó thuộc nơi gọi.
        transaction.Dispose();
        daKetThuc = true;
    }

    // DAO dùng hàm này để không phải lặp try/catch/Commit/Rollback ở mỗi thao tác.
    public static void ThucHien(BookDbContext db, Action thaoTac,
        IsolationLevel mucCoLap = IsolationLevel.ReadCommitted)
    {
        ArgumentNullException.ThrowIfNull(thaoTac);
        using var giaoDich = new DatabaseTransaction(db, mucCoLap);
        try
        {
            thaoTac(); // Nơi gọi thực hiện Add/Update/Delete và SaveChanges.
            giaoDich.Commit();
        }
        catch (Exception ex)
        {
            try
            {
                giaoDich.Rollback();
            }
            catch (Exception loiRollback)
            {
                // Nếu mất kết nối khiến Rollback cũng lỗi, vẫn giữ ngoại lệ ban đầu.
                ex.Data["RollbackException"] = loiRollback;
            }
            throw; // DAL/BLL tiếp tục ném lỗi lên View, không nuốt ngoại lệ.
        }
    }
}
