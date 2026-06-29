using Accounting.DataLayer.Models;
using ValidationComponents;

namespace Accounting.App;

public partial class FrmAddOrEditCustomer : Form
{
    private readonly UnitOfWork db = new();
    private int _id;
    public FrmAddOrEditCustomer(int id = 0)
    {
        InitializeComponent();
        _id = id;
    }

    private void FrmAddOrEditCustomers_Load(object senders, EventArgs e)
    {
        if (_id == 0)
        {
            btnSave.Text = "Create";
        }
        else
        {
            Customer customer = db.CustomerRepository.GetCustomer(_id);
            btnSave.Text = "Edit";
            txtName.Text = customer.FullName;
            txtMobile.Text = customer.Mobile;
            txtAddress.Text = customer.Address;
            txtEmail.Text = customer.Email;
            pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomerImage;
        }
    }

    private void btnSelectPhoto_Click(object senders, EventArgs e)
    {
        OpenFileDialog openFile = new OpenFileDialog();
        if (openFile.ShowDialog() == DialogResult.OK)
        {
            pcCustomer.ImageLocation = openFile.FileName;
        }
    }
    private void no_profile_image(object senders, EventArgs e) { }
    private void btnSave_Click(object senders, EventArgs e)
    {
        if (BaseValidator.IsFormValid(this.components))
        {
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
            string path = Application.StartupPath + "/Images/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            pcCustomer.Image.Save(path + imageName);
            Customer newCuctomer = new Customer()
            {
                FullName = txtName.Text,
                Mobile = txtMobile.Text,
                Address = txtAddress.Text,
                Email = txtEmail.Text,
                CustomerImage = imageName
            };

            if (_id == 0)
            {
                db.CustomerRepository.InserCustomer(newCuctomer);
            }
            else
            {
                newCuctomer.CustomerId = _id;
                db.CustomerRepository.UpdateCustomer(newCuctomer);
            }

            db.Save();
            DialogResult = DialogResult.OK;
        }
    }
}