using System.Windows.Forms;
using Accounting.DataLayer.Models;
using Microsoft.Data.SqlClient;

namespace Accounting.App;

public partial class FrmCustomers : Form
{
    public FrmCustomers()
    {
        InitializeComponent();
    }

    void BindGrid()
    {
        using (UnitOfWork db = new UnitOfWork())
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetAllCustomers();
        }
    }
    private void frmCustomers_Load(object senders, EventArgs e)
    {
        BindGrid();
    }
    private void btnRefreshCustomer_Click(object senders, EventArgs e)
    {
        txtFilter.Text = "";
        BindGrid();
    }
    private void txtFilter_TextChanged(object senders, EventArgs e)
    {
        using (UnitOfWork db = new UnitOfWork())
        {
            var filter = db.CustomerRepository.SearchCustomers(txtFilter.Text);
            dgvCustomers.DataSource = filter;
        }
    }

    private void btnDeleteCustomerr(object senders, EventArgs e)
    {
        if (dgvCustomers.CurrentRow != null)
            using (UnitOfWork db = new UnitOfWork())
            {
                if (MessageBox.Show("do you want to delete this user", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int id = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
                    db.CustomerRepository.DeleteCustomer(id);
                    db.Save();
                    BindGrid();
                }
            }
    }

    private void BtnAddNewCustomer_Click(object sender, EventArgs e)
    {
        FrmAddOrEditCustomer frmAdd = new FrmAddOrEditCustomer();
        if (frmAdd.ShowDialog() == DialogResult.OK)
        {
            BindGrid();
        }
    }
}