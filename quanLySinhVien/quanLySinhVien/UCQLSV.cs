using QuanLySinhVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login
{
    public partial class UCQLSV : UserControl
    {
        public UCQLSV()
        {
            InitializeComponent();
        }

        private void UCQLSV_Load(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            List<tbl_sinhvien> dSSV = db.tbl_sinhviens.ToList();
            dataGridView1.DataSource = dSSV;

            LoadComboBoxGioiTinh();
            LoadComboBoxLop();


        }
        public void LoadComboBoxGioiTinh()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.SelectedIndex = -1;

        }
        public void LoadComboBoxLop()
        {
            DatabaseDataContext db = new DatabaseDataContext();
            List<tbl_lophoc> dSLop = db.tbl_lophocs.ToList();
            cboLop.DataSource = dSLop;
            cboLop.DisplayMember = "tenlop";
            cboLop.ValueMember = "malop";
            cboLop.SelectedIndex = -1;
        }

        private void btlThem_Click(object sender, EventArgs e)
        {
            DatabaseDataContext db = new DatabaseDataContext();
            tbl_sinhvien sv = new tbl_sinhvien();
            sv.masv = txtMaSV.Text;
            sv.hoten = txtHoTen.Text;
            sv.gioitinh = cboGioiTinh.SelectedItem.ToString();
            sv.ngaysinh = dtpNgaySinh.Value;
            sv.malop = cboLop.SelectedValue.ToString();
            db.tbl_sinhviens.InsertOnSubmit(sv);
            db.SubmitChanges();
            List<tbl_sinhvien> dSSV = db.tbl_sinhviens.ToList();
            dataGridView1.DataSource = dSSV;


        }

        private void cboGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            cboLop.SelectedIndex = -1;
            cboGioiTinh.SelectedIndex = -1;
            DatabaseDataContext db = new DatabaseDataContext();
            List<tbl_sinhvien> dSSV = db.tbl_sinhviens.ToList();
            dataGridView1.DataSource = dSSV;
            
           
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // lấy thông tin lên các ô input
                txtMaSV.Text = row.Cells["masv"].Value.ToString();
                txtHoTen.Text = row.Cells["hoten"].Value.ToString();
                cboGioiTinh.SelectedItem = row.Cells["gioitinh"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["ngaysinh"].Value);
                cboLop.SelectedValue = row.Cells["malop"].Value.ToString();
            }
        }
    }
}
