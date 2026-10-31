using QLTVDAO;

namespace QLTVApp;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        // Chỉ tạo mẫu khi chạy với tham số này, không tự thêm khi mở form bình thường.
        if (args.Contains("--du-lieu-mau"))
        {
            try
            {
                DuLieuMau.ThemNeuChuaCo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(DataHelper.LayThongBaoLoi(ex), "Không tạo được dữ liệu mẫu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        Application.Run(new Form1());
    }    
}
