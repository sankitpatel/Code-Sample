using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EFCodeFirst.Model;

namespace EFCodeFirst
{
    public partial class Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        public void BindGrid()
        {
            using (EFContext context = new EFContext())
            {
                //  Using Syntax Based Query
                var customers = from c in context.Customers
                                orderby c.FirstName, c.LastName
                                select new
                {
                    CustomerID = c.CustomerID,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    BirthDate = c.BirthDate,
                    Email = c.Email,
                    Address = c.Address
                };

                //  Using Method Based Query
                //var customers = context.Customers;
                //.Select(l => new
                //{
                //    CustomerID = l.CustomerID,
                //    FirstName = l.FirstName,
                //    LastName = l.LastName,
                //    BirthDate = l.BirthDate,
                //    Email = l.Email,
                //    Address = l.Address
                //});

                //var customers = context.Customers.OrderBy(o => o.FirstName).ThenBy(p => p.LastName).ToList();

                    grdCustomer.DataSource = customers.ToList();
                    grdCustomer.DataBind();
            }
        }

        public void ClearControls()
        {
            hdnCustomerID.Value = "0";
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtBirthDate.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        protected void grdCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDT")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                grdCustomer.SelectedIndex = rowIndex;
                hdnCustomerID.Value = ((HiddenField)grdCustomer.Rows[rowIndex].FindControl("hdnCustomerID")).Value;
                txtFirstName.Text = ((Label)grdCustomer.Rows[rowIndex].FindControl("lblFirstName")).Text;
                txtLastName.Text = grdCustomer.Rows[rowIndex].Cells[1].Text;
                txtBirthDate.Text = grdCustomer.Rows[rowIndex].Cells[2].Text;
                txtEmail.Text = grdCustomer.Rows[rowIndex].Cells[3].Text;
                txtAddress.Text = grdCustomer.Rows[rowIndex].Cells[4].Text;
            }
            else if (e.CommandName == "DLT")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                hdnCustomerID.Value = ((HiddenField)grdCustomer.Rows[rowIndex].FindControl("hdnCustomerID")).Value;
                using (EFContext context = new EFContext())
                {
                    Int64 customerID = Convert.ToInt64(hdnCustomerID.Value);
                    //  Using Syntax Based Query
                    Model.Customer customer = (from c in context.Customers
                                    where c.CustomerID == customerID
                                    select c).SingleOrDefault();
                    context.Customers.Remove(customer);

                    //  Using Method based query
                    //Customer customers = context.Customers.Where(m => m.CustomerID == customerID).SingleOrDefault();
                    //context.Customers.Remove(customers);

                    if (context.SaveChanges() > 0)
                    {
                        BindGrid();
                        ClearControls();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Deleted", "<script>alert('Deleted successfully.');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Deleted", "<script>alert('Error while deleting.');</script>");
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (EFContext context = new EFContext())
                {
                    Model.Customer obj = new Model.Customer();
                    obj.CustomerID = Convert.ToInt64(hdnCustomerID.Value);
                    obj.FirstName = txtFirstName.Text.Trim();
                    obj.LastName = txtLastName.Text.Trim();
                    obj.BirthDate = Convert.ToDateTime(txtBirthDate.Text.Trim(), System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                    obj.Email = txtEmail.Text.Trim();
                    obj.Address = txtAddress.Text.Trim();

                    if (Convert.ToInt64(hdnCustomerID.Value) > 0)
                    {
                        context.Entry(obj).State = EntityState.Modified;      //To Update all fields

                        ////To Update Induvidual fields
                        //context.Customers.Attach(obj);
                        //context.Entry(obj).Property(u => u.FirstName).IsModified = true;
                        //context.Entry(obj).Property(u => u.LastName).IsModified = true;
                    }
                    else
                    {
                        context.Customers.Add(obj);
                    }
                    if (context.SaveChanges() > 0)
                    {
                        var customerID = obj.CustomerID;
                        BindGrid();
                        ClearControls();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Saved", "<script>alert('Saved successfully.');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Saved", "<script>alert('Error while saving.');</script>");
                    }
                }
            }
        }
    }
}