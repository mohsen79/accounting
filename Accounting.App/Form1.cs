namespace Accounting.App;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void btnCustomers_Click(object senders, EventArgs e)
    {
        FrmCustomers frm = new FrmCustomers();
        frm.ShowDialog();
    }
}
