using System;
using System.Windows.Forms;

namespace Ulix
{
    public partial class UlixHelp : Form
    {
        public UlixHelp()
        {
            InitializeComponent();
        }
        private void CloseAppControlBox_Click(object sender, EventArgs e) => Hide();
        private async void UlixHelp_Load(object sender, EventArgs e)
        {
            await helpPage.EnsureCoreWebView2Async(null);

            helpPage.CoreWebView2.ContextMenuRequested += (sender1, args) =>
            {
                args.Handled = true;
            };
        }
    }
}