using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLibraryAddBitStream
{
    class Program
    {
        static OdbcConnection con1 = new OdbcConnection("Driver=Sybase ASE ODBC Driver;SRVR=production;DB=kfnl;UID=sa;PWd=sybase1;");
        static NpgsqlConnection pgconn = new NpgsqlConnection("Host=192.168.1.178;Username=dspace;Password=dspace;Database=dspace");
        static string dublin_core = "dublin_core.xml";
        static string contents = "contents";
        static void Main(string[] args)
        {
            new Program().addDLPolicy();
            Console.ReadKey();
        }


        //public void addCoverFile()
        //{
        //    // string sourceFolderPath = @"\\192.168.1.112\مشروع المكتبة الرقمية\أعمال المعالجة\مقالات تم رقعها\splited_final\";
        //    string distFolderName = @"C:\DLMigrate\DLDEPTTCD_AHLAM";
        //    string distSubFolderName = @"\1";
        //    //string pathString = System.IO.Path.Combine(distFolderName, "SubFolder");
        //    try
        //    {
        //        con1.Open();
        //        pgconn.Open();

        //        OdbcCommand commandItem2 = new OdbcCommand(@"select bib#,FileName, filePath from DLDEPTTCD_AHLAM ", con1);

        //        DataSet resultsItem2 = new DataSet();
        //        OdbcDataAdapter usersAdapterItem2 = new OdbcDataAdapter(commandItem2);
        //        usersAdapterItem2.Fill(resultsItem2);
        //        DataTable dtItem2 = resultsItem2.Tables[0];
        //        int count = 1;
        //        int subfile = 1;
        //        foreach (DataRow rowItem2 in dtItem2.Rows)
        //        {

        //            if (count == 1000)
        //            {
        //                distSubFolderName = @"\" + subfile;
        //                count = 1;
        //                subfile++;
        //            }
        //            Console.WriteLine("working on bib# = " + rowItem2["bib#"]);
        //            string pathString = System.IO.Path.Combine(distFolderName + distSubFolderName, rowItem2["bib#"].ToString()) + "\\";
        //            if (!System.IO.Directory.Exists(pathString))
        //            {
        //                System.IO.Directory.CreateDirectory(pathString);

        //            }
        //            else
        //            {
        //                try
        //                {
        //                    count++;
        //                    string path = rowItem2["filePath"].ToString().ToLower() + @"\" + rowItem2["filename"].ToString().ToLower();
        //                    Encoding utf8 = Encoding.GetEncoding("Windows-1252");

        //                    Console.WriteLine(path);
        //                    System.IO.File.Copy(path, pathString + rowItem2["filename"].ToString().ToLower(), true);
        //                    System.IO.File.Move(pathString + rowItem2["filename"].ToString().ToLower(), pathString + rowItem2["filename"].ToString().ToLower().Replace(".pdf", "-FullText.pdf"));
        //                    System.IO.File.Delete(pathString + rowItem2["filename"].ToString().ToLower());
        //                    createContents(rowItem2["filename"].ToString().ToLower().Replace(".pdf", "-FullText.pdf"), pathString);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine("-------- error : {0}", ex.Message);
        //                }
        //                continue;
        //            }
        //            try
        //            {
        //                count++;
        //                string path = rowItem2["filePath"].ToString().ToLower() + @"\" + rowItem2["filename"].ToString().ToLower();

        //                Console.WriteLine(path);
        //                System.IO.File.Copy(path, pathString + rowItem2["filename"].ToString().ToLower(), true);
        //                System.IO.File.Move(pathString + rowItem2["filename"].ToString(), pathString + rowItem2["filename"].ToString().ToLower().Replace(".pdf", "-FullText.pdf"));
        //                System.IO.File.Delete(pathString + rowItem2["filename"].ToString().ToLower());
        //                createDublincCore(rowItem2["bib#"].ToString(), pathString);
        //                createContents(rowItem2["filename"].ToString().ToLower().Replace(".pdf", "-FullText.pdf"), pathString);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("-------- error : {0}", ex.Message);
        //            }
        //            count++;
        //        }
        //        Console.ReadKey();
        //    }



        //    finally
        //    {
        //        con1.Close();
        //        con1.Dispose();
        //        pgconn.Close();
        //        pgconn.Dispose();
        //    }
        //    //bibCheckInDSpace();
        //}
        public void addFullTextFile()
        {
            // string sourceFolderPath = @"\\192.168.1.112\مشروع المكتبة الرقمية\أعمال المعالجة\مقالات تم رقعها\splited_final\";
            string distFolderName = @"H:\DLMigrate\DLBooks_19_05_2020\Full\";
            string distSubFolderName = @"\1";
            //string pathString = System.IO.Path.Combine(distFolderName, "SubFolder");
            try
            {
                con1.Open();
                pgconn.Open();

                OdbcCommand commandItem2 = new OdbcCommand(@"select * from dlibrarybibsnofound_19_05_2020", con1);

                DataSet resultsItem2 = new DataSet();
                OdbcDataAdapter usersAdapterItem2 = new OdbcDataAdapter(commandItem2);
                usersAdapterItem2.Fill(resultsItem2);
                DataTable dtItem2 = resultsItem2.Tables[0];
                int count = 1;
                int subfile = 1;
                foreach (DataRow rowItem2 in dtItem2.Rows)
                {
                    string path = rowItem2["filePath"].ToString().ToLower() + @"\" + rowItem2["filename"].ToString().ToLower();

                    if (count == 1000)
                    {
                        distSubFolderName = @"\" + subfile;
                        count = 1;
                        subfile++;
                    }
                    Console.WriteLine("working on bib# = " + rowItem2["bib#"]);
                    string pathString = System.IO.Path.Combine(distFolderName + distSubFolderName, rowItem2["bib#"].ToString()) + "\\";
                    if (!System.IO.Directory.Exists(pathString))
                    {
                        System.IO.Directory.CreateDirectory(pathString);

                    }
                    else
                    {
                        try
                        {
                            count++;
                            CopyFileWithCreateContents(path, pathString, rowItem2["filename"].ToString());
                          
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("-------- error : {0}", ex.StackTrace);
                        }
                        continue;
                    }
                    try
                    {
                        CopyFileWithCreateContents(path, pathString, rowItem2["filename"].ToString());
                        createDublincCore(rowItem2["bib#"].ToString(), pathString);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("-------- error : {0}", ex.StackTrace);
                    }
                    count++;
                }
            }



            finally
            {
                con1.Close();
                con1.Dispose();
                pgconn.Close();
                pgconn.Dispose();
            }
            //bibCheckInDSpace();
        }
        public void addDLPolicy()
        {
            // string sourceFolderPath = @"\\192.168.1.112\مشروع المكتبة الرقمية\أعمال المعالجة\مقالات تم رقعها\splited_final\";

            //string pathString = System.IO.Path.Combine(distFolderName, "SubFolder");
            try
            {

                pgconn.Open();



                NpgsqlCommand commandItem2 = new NpgsqlCommand(@"select distinct(DSPACE_OBJECT) from tempaddedpolicy", pgconn);

                DataSet resultsItem2 = new DataSet();
                NpgsqlDataAdapter usersAdapterItem2 = new NpgsqlDataAdapter(commandItem2);
                usersAdapterItem2.Fill(resultsItem2);
                DataTable dtItem2 = resultsItem2.Tables[0];
                int count = 0;
                foreach (DataRow rowItem2 in dtItem2.Rows)
                {

                    count++;
                    var cmd = new NpgsqlCommand("INSERT INTO public.resourcepolicy(policy_id, resource_type_id, action_id, epersongroup_id, dspace_object) VALUES((select nextval ('public.resourcepolicy_seq')),0,0,'21204304-c627-4963-8d0d-493f34c53bc8',@p); ", pgconn);

                    cmd.Parameters.AddWithValue("p", rowItem2[0]);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine(count + "     " + rowItem2[0]);


                }
            }



            finally
            {

                pgconn.Close();
                pgconn.Dispose();
            }
            //bibCheckInDSpace();
        }
        public static void createDublincCore(string bibNo, string distFolder)
        {
            NpgsqlCommand command = new NpgsqlCommand("select dspace_object_id from public.metadatavalue where  metadata_field_id =190 and text_value = @p", pgconn);
            command.Parameters.AddWithValue("p", bibNo);
            string url = "";

            DataSet resultsItem1 = new DataSet();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            adapter.Fill(resultsItem1);
            DataTable dtItem = resultsItem1.Tables[0];
            foreach (DataRow rowItem in dtItem.Rows)
            {
                Console.WriteLine("----------------------------------hghgfshgfhgfh-------------------------");
                NpgsqlCommand command2 = new NpgsqlCommand("select text_value from public.metadatavalue where  metadata_field_id =31 and dspace_object_id = @p", pgconn);
                command2.Parameters.AddWithValue("p", rowItem[0]);
                DataSet resultsItem2 = new DataSet();
                NpgsqlDataAdapter adapter2 = new NpgsqlDataAdapter(command2);
                adapter2.Fill(resultsItem2);
                DataTable dtItem2 = resultsItem2.Tables[0];
                foreach (DataRow rowItem2 in dtItem2.Rows)
                {

                    url = rowItem2[0].ToString();
                    //if (!url.ToLower().StartsWith("https://dlibrary.kfnl.gov.sa"))
                    //{
                    //    NpgsqlCommand command3 = new NpgsqlCommand("insert into urierrors (dspace_object_id) values(@p)", pgconn);
                    //    command3.Parameters.AddWithValue("p", rowItem[0]);
                    //    command3.ExecuteNonQuery();
                    //}
                }
            }

            string part1 = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?>\n";
            string part2 = "<dublin_core schema = \"dc\">\n";
            string part3 = "  <dcvalue element=\"identifier\" qualifier=\"uri\">" + url + "</dcvalue>\n";
            string part4 = "</dublin_core>";
            System.IO.File.WriteAllText(distFolder + dublin_core, part1 + part2 + part3 + part4);
            Console.WriteLine("--dublin_core.xml-- created");
        }

        public static void createContents(string fileName, string distFolder, FileType type)
        {
            string text = string.Empty;
            if (type == FileType.FullText)
                text = fileName + "\tbundle: ORIGINAL\tdescription:المحتوى الرقمي";

            else if (type == FileType.Content)
                text = fileName + "\tbundle: ORIGINAL\tdescription:جدول المحتويات"; 
            if (!System.IO.File.Exists(distFolder + contents))
            {

                System.IO.File.WriteAllText(distFolder + contents, text + "\n");
            }
            else
            {
                using (System.IO.StreamWriter file =
          new System.IO.StreamWriter(distFolder + contents, true))
                {

                    // If the line doesn't contain the word 'Second', write the line to the file.

                    file.WriteLine(text);


                }
            }

            Console.WriteLine("--Contents-- created");
        }

        public static void CopyFileWithCreateContents(string path, string pathString, string fileName)
        {
            if (fileName.ToLower().Contains("-con"))
            {
                System.IO.File.Copy(path, pathString + fileName.ToLower(), true);
                createContents(fileName.ToLower(), pathString, FileType.Content);
            }
            else
            {
                System.IO.File.Copy(path, pathString + fileName.ToLower(), true);
                System.IO.File.Move(pathString + fileName.ToLower(), pathString + fileName.ToLower().Replace(".pdf", "-FullText.pdf"));
                System.IO.File.Delete(pathString + fileName.ToLower());
                createContents(fileName.ToLower().Replace(".pdf", "-FullText.pdf"), pathString, FileType.FullText);
            }
        }

        public enum FileType
        {
            FullText = 1,
            Content = 2,
            Cover = 3
        }
        public static void bibCheckInDSpace()
        {



            try
            {
                con1.Open();
                pgconn.Open();

                OdbcCommand commandItem2 = new OdbcCommand(@"select bib#,FileName from DLPart1", con1);

                DataSet resultsItem2 = new DataSet();
                OdbcDataAdapter usersAdapterItem2 = new OdbcDataAdapter(commandItem2);
                usersAdapterItem2.Fill(resultsItem2);
                DataTable dtItem2 = resultsItem2.Tables[0];
                int count = 1;
                foreach (DataRow rowItem2 in dtItem2.Rows)
                {
                    Console.WriteLine(count + " working on bib# = " + rowItem2["bib#"]);
                    count++;


                    NpgsqlCommand command = new NpgsqlCommand("select * from public.bibs where  text_value =  @p", pgconn);
                    command.Parameters.AddWithValue("p", rowItem2["bib#"]);
                    NpgsqlDataReader s = command.ExecuteReader();


                    //DataSet resultsItem1 = new DataSet();
                    //NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    //adapter.Fill(resultsItem1);
                    //DataTable dtItem = resultsItem1.Tables[0];
                    if (!s.HasRows)
                    {
                        OdbcCommand commandItem3 = new OdbcCommand(@"insert into DLPart1_NotFound (bib#,filename,filepath) values (?,'1','1')", con1);
                        commandItem3.Parameters.Add(new OdbcParameter("@bib#", rowItem2["bib#"]));
                        commandItem3.ExecuteNonQuery();
                    }
                    s.Close();

                }
            }



            finally
            {
                con1.Close();
                con1.Dispose();
                pgconn.Close();
                pgconn.Dispose();
            }
        }

    }
}
