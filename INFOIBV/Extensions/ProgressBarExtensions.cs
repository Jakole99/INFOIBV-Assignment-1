namespace INFOIBV.Extensions;

public static class ProgressBarExtensions
{
    public static void Setup(this ProgressBar progressBar, int total)
    {
        progressBar.Visible = true;
        progressBar.Minimum = 0;
        progressBar.Maximum = total - 1;
        progressBar.Value = 0;
        progressBar.Step = 1;
    }
}