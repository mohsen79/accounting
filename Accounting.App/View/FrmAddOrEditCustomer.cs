using Accounting.DataLayer.Models;
using ValidationComponents;

namespace Accounting.App;

public partial class FrmAddOrEditCustomer : Form
{
    private readonly UnitOfWork db = new();
    public FrmAddOrEditCustomer()
    {
        InitializeComponent();
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

            db.CustomerRepository.InserCustomer(newCuctomer);
            db.Save();
            DialogResult = DialogResult.OK;
        }
    }
}