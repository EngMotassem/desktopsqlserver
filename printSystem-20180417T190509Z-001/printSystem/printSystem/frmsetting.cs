using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
namespace printSystem
{
    public partial class frmsetting : MetroFramework.Forms.MetroForm
    {

        //String department;
        public frmsetting()
        {
            InitializeComponent();
            

        }
     


        dataDataSetTableAdapters.examTable1TableAdapter adabexam = new dataDataSetTableAdapters.examTable1TableAdapter();
        dataDataSetTableAdapters.studentData1TableAdapter adabstu = new dataDataSetTableAdapters.studentData1TableAdapter();
        dataDataSetTableAdapters.decTbl1TableAdapter adabdec = new dataDataSetTableAdapters.decTbl1TableAdapter();
        dataDataSetTableAdapters.decarchiveTableAdapter archeive = new dataDataSetTableAdapters.decarchiveTableAdapter();
        dataDataSet.examTable1DataTable list = new dataDataSet.examTable1DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataDataSet1.decTbl1' table. You can move, or remove it, as needed.
            this.decTbl1TableAdapter.Fill(this.dataDataSet1.decTbl1);

        }

        private void cmbcourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbcourse_SelectedValueChanged(object sender, EventArgs e)
        {

           

            if (cmbcourse.SelectedIndex != -1)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].examId == Convert.ToInt16(cmbcourse.SelectedValue))
                    {
                        string day =  list[i].examDate.Date.Day.ToString();
                        string month =  list[i].examDate.Date.Month.ToString();
                        string year =  list[i].examDate.Date.Year.ToString();



                        txtDate.Text = year+"/"+month+"/"+day;
                        txtDay.Text = list[i].examday;
                        break;
                    }
                }
            }
        }

        public int contain()
        {






            try
            {

                adabdec.Fill(dataDataSet1.decTbl1);

                dataDataSet.decTbl1DataTable list = dataDataSet1.decTbl1;

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].decNumber == txtnumherman.Text && list[i].seatNo==txtseatNo.Text)
                    {

                        //MetroMessageBox.Show(this, "check name and id fun","found ", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);

                        return 1;

                    }

                }
                //MetroMessageBox.Show(this, "check name and id fun", "Not found ", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);

                return 0;
            }
            catch (Exception)
            {

                return 0;
            }
        }




        public int checknameandid()
        {

          int a=  Convert.ToInt32( adabdec.ConfictCount(txtseatNo.Text, cmbType.Text, txtnumherman.Text));

            /*

             try
             {

                 adabdec.Fill(dataDataSet1.decTbl1);

                 dataDataSet.decTbl1DataTable list = dataDataSet1.decTbl1;

                 for (int i = 0; i < list.Count; i++)
                 {
                     if (list[i].decNumber == txtnumherman.Text && list[i].stName != txtname.Text)
                     {
                         return 1;
                     }

                 }
                 return 0;
             }
             catch (Exception)
             {

                 return 0;
             }
 */

          //  MetroMessageBox.Show(this, "check name and id fun", a.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);

            return a;

        }

        private void btnadd_Click(object sender, EventArgs e)
        {

        }
        string examscname = "";
        private void metroButton1_Click(object sender, EventArgs e)
        {
            // البحث عن طالب
            try
            {
                if (txtseatNo.Text != "")
                {

                    int count = adabstu.FillBy(dataDataSet1.studentData1, Convert.ToDouble(txtseatNo.Text));
                    if (count != 0)
                    {

                        txtname.Text = dataDataSet1.studentData1.Rows[0]["stname"].ToString();
                        txtschool.Text = dataDataSet1.studentData1.Rows[0]["scname"].ToString();
                        regionText.Text = dataDataSet1.studentData1.Rows[0]["examScid"].ToString();
                        seerTxt.Text= dataDataSet1.studentData1.Rows[0]["examscname"].ToString();
                        lagnaNumTxt.Text= dataDataSet1.studentData1.Rows[0]["seercode"].ToString();
                        typedesc.Text= dataDataSet1.studentData1.Rows[0]["type_adesc"].ToString();
                        stType.Text = dataDataSet1.studentData1.Rows[0]["st_type"].ToString();
                        depatText.Text= dataDataSet1.studentData1.Rows[0]["typename"].ToString();
                        //gender = dataDataSet1.studentData.Rows[0]["sex"];

                        if (cmbcourse.SelectedIndex == -1)
                        adabexam.FillBy(dataDataSet1.examTable1, Convert.ToInt32(dataDataSet1.studentData1.Rows[0]["typecode"].ToString()));
                        cmbcourse.DisplayMember = "examName";
                        cmbcourse.ValueMember = "examId";
                        list = dataDataSet1.examTable1;
                         cmbcourse.DataSource = list;
                        examscname = dataDataSet1.studentData1.Rows[0]["examscname"].ToString();
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "رقم الجلوس غير مسجل بالنظام", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);

                    }
                }
                else
                {
                   MetroMessageBox.Show(this, "من فضلك أدخل رقم الجلوس", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);

                }

            }
            catch (Exception g)
            {


            }

        }

        private void txtseatNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // keypress
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
               
            }
        }

        private void txtnumherman_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public void show()
        {
            try
            {

                adabdec.Fill(dataDataSet1.decTbl1);
                devgrid.Rows.Clear();
                dataDataSet.decTbl1DataTable list = dataDataSet1.decTbl1;

                for (int i = 0; i < list.Count; i++)
                {
                    devgrid.Rows.Add();
                    devgrid.Rows[i].Cells[0].Value = list[i].decID;
                    devgrid.Rows[i].Cells[1].Value = i + 1;
                    devgrid.Rows[i].Cells[2].Value = list[i].seatNo;
                    devgrid.Rows[i].Cells[3].Value = list[i].stName;
                    devgrid.Rows[i].Cells[4].Value = list[i].decNumber;
                    devgrid.Rows[i].Cells[7].Value = list[i].region;
                }
            }
            catch (Exception)
            {


            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
      
        }



        private void devgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    // عرض قرار سابق من الجريد
                    if (e.ColumnIndex == 5)
                    {
                        adabdec.FillBy(dataDataSet1.decTbl1, Convert.ToInt32(devgrid.Rows[e.RowIndex].Cells[0].Value));
                        dataDataSet.decTbl1DataTable list1 = dataDataSet1.decTbl1;
                        if (list1.Count != 0)
                        {
                            txtname.Text = list1[0].stName;
                            txtschool.Text = list1[0].scName;
                            txtseatNo.Text = list1[0].seatNo.ToString();
                            txtnumherman.Text = list1[0].decNumber;
                            cmbType.Text = list1[0].decType;
                            txtreport.Text = list1[0].dec_desc;
                            regionText.Text = list1[0].region;
                            seerTxt.Text = list1[0].examscName;

                           

                            lagnaNumTxt.Text = list1[0].type_str;


                           






                            adabstu.FillBy(dataDataSet1.studentData1, Convert.ToInt32(txtseatNo.Text));
                            
                            adabexam.FillBy(dataDataSet1.examTable1, Convert.ToInt32(dataDataSet1.studentData1.Rows[0][4].ToString()));
                            cmbcourse.DisplayMember = "examName";
                            cmbcourse.ValueMember = "examId";
                            list = dataDataSet1.examTable1;
                            cmbcourse.DataSource = list;


                            cmbcourse.Text = list1[0].examName;
                            txtDay.Text = list1[0].examday;
                            txtDate.Text = list1[0].examDate;
                        }
                    }

                    // حذف قرار
                    else if (e.ColumnIndex == 6)
                    {
                        int test = 0;
                        DialogResult res = MetroMessageBox.Show(this, "هل أنت متأكد من حذف هذا القرار \n مع العلم  سيتم حذفه نهائياَ؟", "معلومات", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 150);
                        if (res == DialogResult.Yes)
                        {
                            adabdec.FillBy(dataDataSet1.decTbl1, Convert.ToInt32(devgrid.Rows[e.RowIndex].Cells[0].Value));
                            dataDataSet.decTbl1DataTable list = dataDataSet1.decTbl1;
                            if (list.Count != 0)
                            {
                                //if (list[0].type_str is null)
                                //{

                                

                               int  add = archeive.InsertQuery(1, Int32.Parse(list[0].decNumber), Int32.Parse(list[0].seatNo), list[0].examName, list[0].examDate, list[0].decType, list[0].dec_desc);

                                //int add = archeive.InsertQuery(1,1, 233, "fuufuf", "ffff", 2333, "rrrrrr");


                                if (add==1)
                                    MetroMessageBox.Show(this, "added to archeieve ", "معلومات ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 100);



                                test = adabdec.Delete( list[0].seatNo,list[0].decNumber);
                                //}
                                //else
                                //{
                                  //  test = adabdec.Delete(list[0].decNumber);
                                    //  test = adabdec.Delete(list[0].decID, list[0].seatNo, list[0].stName, list[0].scName, list[0].examscName, list[0].examName, list[0].examday, list[0].examDate, list[0].decType, list[0].dec_desc, list[0].decNumber, list[0].region, list[0].type_str,list[0].type_adesc,list[0]);


                                //}
                                if (test == 1)
                                {
                                    MetroMessageBox.Show(this, "تم حذف البيانات بنجاح", "معلومات ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 100);

                                    show();
                                }

                            }
                        }
                    }

                }
            }
            catch (Exception v)
            {
            }
        }
        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                adabdec.FillByname(dataDataSet1.decTbl1, txtsearch.Text);

                devgrid.Rows.Clear();
                dataDataSet.decTbl1DataTable list = dataDataSet1.decTbl1;

                for (int i = 0; i < list.Count; i++)
                {
                    devgrid.Rows.Add();
                    devgrid.Rows[i].Cells[0].Value = list[i].decID;
                    devgrid.Rows[i].Cells[1].Value = i + 1;
                    devgrid.Rows[i].Cells[2].Value = list[i].seatNo;
                    devgrid.Rows[i].Cells[3].Value = list[i].stName;
                    devgrid.Rows[i].Cells[4].Value = list[i].decNumber;
                }
            }
            catch (Exception)
            {


            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            try
            {
                if (devgrid.Rows.Count != -1)
                {

                }
            }
            catch (Exception)
            {
            }
        }


        // طباعة قرار الحرمان

        private void btnprintqarar_Click(object sender, EventArgs e)
        {

            // cofigure data 

            DataTable dt = new DataTable();
            dt.Columns.Add("seatno", typeof(string));
            dt.Columns.Add("studentname", typeof(string));

            dt.Columns.Add("school", typeof(string));
            dt.Columns.Add("subject", typeof(string));
            dt.Columns.Add("description", typeof(string));


            dt.Columns.Add("seername", typeof(string));
            dt.Columns.Add("examname", typeof(string));
            dt.Columns.Add("examdate", typeof(string));
            dt.Columns.Add("examday", typeof(string));
            dt.Columns.Add("depart", typeof(string));
            dt.Columns.Add("period", typeof(string));
            dt.Columns.Add("year", typeof(string));
            dt.Columns.Add("number", typeof(string));
            dt.Columns.Add("region", typeof(string));
            dt.Columns.Add("seercode", typeof(string));

            dt.Rows.Add(new object[] { txtseatNo.Text, txtname.Text, txtschool.Text, cmbcourse.Text, txtreport.Text, seerTxt.Text, cmbcourse.Text, txtDate.Text, txtDay.Text, "", "", "", txtnumherman.Text, regionText.Text, lagnaNumTxt.Text });
            int test = 0;



            try
            {

                if (txtname.Text != "" && txtschool.Text != "" && txtnumherman.Text != "" && cmbType.Text != "" && cmbcourse.SelectedIndex != -1 && txtDate.Text != "" && txtDay.Text != "" && txtreport.Text != "")
                {

                    // found in grid

                    if (contain() == 1)
                    {

                        //adabdec.UpdateQuery1(txtreport.Text, txtnumherman.Text);




                        printreport(dt);

                    }



                    // else not contain 
                    else
                    {


                        if (checknameandid() == 1)
                        {
                            // show error message 

                            MetroMessageBox.Show(this, "هذا الرقم مسجل لطالب اخر", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 100);

                            txtnumherman.Text = "";
                            return;
                        }



                        else
                        {
                            if (addtodb(dt) == 1)
                            {
                                MetroMessageBox.Show(this, "تم إدخال البيانات بنجاح", "معلومات ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 100);

                                printreport(dt);
                                clear();
                                show();

                            }
                        }


                    }
                }
                else
                {
                    MetroMessageBox.Show(this, "من فضلك أكمل البيانات أولا", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 100);

                }
            }
            catch (Exception ff)
            {
                if (test == 1)
                {

                    MetroMessageBox.Show(this, "تم إدخال البيانات بنجاح", "معلومات ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 100);
                    frmReportsMahader f = new frmReportsMahader(dt);

                    clear();
                    show();
                }

            }

        }


        private void clear()
        {
            int hermann=0;
            txtname.Text  = txtschool.Text = txtseatNo.Text = regionText.Text=seerTxt.Text= "";

            if (txtnumherman.Text != "") {
                hermann = Convert.ToInt32(txtnumherman.Text);

                txtnumherman.Text = (hermann + 1).ToString();


            }

                // hermann = Convert.ToInt32(txtnumherman.Text);

          
        }

        private void cmbcourse_DropDown(object sender, EventArgs e)
        {

        }




        // ورقة مسحوبة
       private void btnprintgolaph_Click(object sender, EventArgs e)
        {


            



            DataTable dt = new DataTable();
            dt.Columns.Add("seatno", typeof(string));
            dt.Columns.Add("studentname", typeof(string));

            dt.Columns.Add("school", typeof(string));
            dt.Columns.Add("subject", typeof(string));

            dt.Columns.Add("seername", typeof(string));
            dt.Columns.Add("examname", typeof(string));
            dt.Columns.Add("examdate", typeof(string));
            dt.Columns.Add("examday", typeof(string));
            dt.Columns.Add("depart", typeof(string));
            dt.Columns.Add("period", typeof(string));
            dt.Columns.Add("year", typeof(string));
            dt.Columns.Add("number", typeof(string));
            dt.Columns.Add("typedesc", typeof(string));




            dt.Rows.Add(new object[] { txtseatNo.Text, txtname.Text, txtschool.Text,cmbcourse.Text, examscname, cmbcourse.Text, txtDate.Text, txtDay.Text,"","","",txtnumherman.Text, metroComboBox1.Text});


            if (txtname.Text != "")
            {

                frmReports sh = new frmReports(dt);

                show();

                clear();
            }
            else
            {
                MetroMessageBox.Show(this, "من فضلك اكمل البيانات", "معلومات ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 100);


            }


        }

        private void chkprint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkprint.Checked && devgrid.Rows.Count!=0)
            {
                frmReportsWay f = new printSystem.frmReportsWay(Convert.ToInt32(devgrid.Rows[devgrid.CurrentCellAddress.Y].Cells[1].Value));
            }
        }

        private void metroButton2_Click_1(object sender, EventArgs e)
        {
            show();
        }

        private void txtseatNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                metroButton1_Click(null, null);
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void subjectStaticsmetroButton_Click(object sender, EventArgs e)
        {
            staticsFormCollect form = new staticsFormCollect();
            form.Show();
        }










        private int addtodb(DataTable dt)
        {
            int test = 0;


            String gender = dataDataSet1.studentData1.Rows[0]["sex"].ToString();
            String decCode = "---";


            if (cmbType.SelectedIndex == 1)
            {
                decCode = "2";

                // حرمان جميع المجالات  نموذج 2


            }
            else if (cmbType.SelectedIndex == 2)
            {
                // حرمان من مجال دراسي   نموذج 4
                decCode = "1";


            }
            else if (cmbType.SelectedIndex == 3)
            {
                // حرمان من جميع المجالات  نموذج 6

                decCode = "2";


                //  


            }
            else if (cmbType.SelectedIndex == 4)
            {
                // حرمان من جميع المجالات  نموذج 7

                decCode = "3";


                // f.Show();

            }


            test = adabdec.Insert(txtseatNo.Text, txtname.Text, txtschool.Text, examscname, cmbcourse.Text, txtDay.Text, txtDate.Text, cmbType.Text, txtreport.Text, txtnumherman.Text, regionText.Text, lagnaNumTxt.Text, typedesc.Text, Int32.Parse(stType.Text), Int32.Parse(gender), decCode);




            return test;

        }





        // print report fun
        private void printreport(DataTable dt)
        {

            if (cmbType.SelectedIndex == 1)
            {
                // حرمان مجال جميع المجالات نموذج 2
                frmReportsMahader f = new frmReportsMahader(dt);

            }
            else if (cmbType.SelectedIndex == 2)
            {
                // حرمان من مجال دراسي  نموذج 4
                frmReportsMahader2 f = new frmReportsMahader2(dt);

            }
            else if (cmbType.SelectedIndex == 3)
            {
                // حرمان من جميع المجالات  نموذج 6
                frmReportsMahader3 f = new frmReportsMahader3(dt);

                //  


            }
            else if (cmbType.SelectedIndex == 4)
            {
                // حرمان من جميع المجالات  نموذج 7


                finalFormmahder f = new finalFormmahder(dt);
                // f.Show();

            }
        }




        private int  insertnewreport()
        {

            String gender = dataDataSet1.studentData1.Rows[0]["sex"].ToString();


            String decCode = "---";


            if (cmbType.SelectedIndex == 1)
            {
                decCode = "2";

                // حرمان جميع المجالات  نموذج 2


            }
            else if (cmbType.SelectedIndex == 2)
            {
                // حرمان من مجال دراسي   نموذج 4
                decCode = "1";


            }
            else if (cmbType.SelectedIndex == 3)
            {
                // حرمان من جميع المجالات  نموذج 6

                decCode = "2";


                //  


            }
            else if (cmbType.SelectedIndex == 4)
            {
                // حرمان من جميع المجالات  نموذج 7

                decCode = "3";


                // f.Show();

            }




            int a = adabdec.Insert(txtseatNo.Text, txtname.Text, txtschool.Text, examscname, cmbcourse.Text, txtDay.Text, txtDate.Text, cmbType.Text, txtreport.Text, txtnumherman.Text, regionText.Text, lagnaNumTxt.Text, typedesc.Text, Convert.ToDouble(stType.Text), Convert.ToDouble(gender), decCode);





            return a;

        }






    }
}
