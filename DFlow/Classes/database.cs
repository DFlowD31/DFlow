using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using Microsoft.VisualBasic.CompilerServices;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Collections;

namespace DFlow
{
    //public static class DataRowExtensions
    //{
    //    public static T FieldOrDefault<T>(this DataRow row, string columnName)
    //    {
    //        return row.IsNull(columnName) ? default : row.Field<T>(columnName);
    //    }
    //    public static T FieldOrDefault<T>(this DataRow row, int columnIndex)
    //    {
    //        return row.IsNull(1) ? default : row.Field<T>(columnIndex);
    //    }
    //}
    class database
    {
        public readonly SQLiteConnection sqlConnection = new SQLiteConnection("Data Source=" + Application.StartupPath + @"\database\data.sqlite; New=False;");
        private DataTable sqlDataTable;
        private DataSet sqlDataSet;

        public bool Check_Connection()
        {
            bool return_value = false;
            try
            {
                sqlConnection.Open();
                if (sqlConnection.State == ConnectionState.Open)
                    return_value = true;
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.ToString(), "Error"); return_value = false; }
            finally { sqlConnection.Close(); }
            return return_value;
        }

        #region By String
        public bool Insert_Data_For_Database(List<string> Parameter_Names, List<string> Parameter_Values, string Table_Name, ref int Unique_Id)
        {
            bool returnValue = false;
            try
            {
                sqlConnection.Open();
                int num = 0;
                string str = "";
                string str2 = "";
                do
                {
                    str += "`" + Parameter_Names[num].ToString() + "`";
                    str2 += "'" + Parameter_Values[num].ToString() + "'";
                    if (num < Parameter_Names.Count - 1)
                    {
                        str += ", ";
                        str2 += ", ";
                    }
                    num += 1;
                } while (num < Parameter_Names.Count());
                using (SQLiteCommand sqlCommand = new SQLiteCommand("INSERT INTO `" + Table_Name + "`(" + str + ") VALUES (" + str2 + ")", sqlConnection))
                    sqlCommand.ExecuteNonQuery();
                using (SQLiteCommand sqlCommand = new SQLiteCommand("SELECT @@Identity", sqlConnection))
                    Unique_Id = Convert.ToInt32(sqlCommand.ExecuteScalar());
                returnValue = true;
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.ToString(), "Error"); }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close(); }
            return returnValue;
        }

        public string Get_From_Database(string txt, bool Count = false)
        {
            string Return_Value = "";
            try
            {
                sqlConnection.Open();
                using (SQLiteCommand sqlCommand = new SQLiteCommand(txt, sqlConnection))
                {
                    using (SQLiteDataReader sqlReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlReader.Read())
                            Return_Value = sqlReader.GetInt64(0).ToString();
                    }
                }
            }
            catch (Exception ex) { Return_Value = ""; Program.Main_Form.Log(ex.ToString(), "Error"); }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close(); }
            return Return_Value;
        }

        public DataTable Get_Multiple_From_Database(List<string> Get_Values, string Table_Name, string Condition)
        {
            string command_text = "";
            try
            {
                if (Get_Values[0] == "*")
                    command_text = "*";
                else
                {
                    foreach (string items in Get_Values)
                        command_text += "`" + items + "`, ";
                    command_text = command_text.Substring(0, command_text.LastIndexOf(","));
                }
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.ToString(), "Error"); }
            try
            {
                if (Condition == "``=''" || Condition == "`` LIKE '%%'" || Condition == "`` LIKE ''")
                    command_text = "SELECT " + command_text + " FROM `" + Table_Name + "`";
                else
                    command_text = "SELECT " + command_text + " FROM `" + Table_Name + "` WHERE " + Condition;
                sqlConnection.Open();
                using (SQLiteDataAdapter sqlAdapter = new SQLiteDataAdapter(new SQLiteCommand("SELECT * FROM `movies`", sqlConnection)))
                    sqlAdapter.Fill(sqlDataTable = new DataTable());
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.ToString(), "Error"); }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Open(); }
            return sqlDataTable;
        }

        public bool Update_Data_For_Database_WithString(string CommandText)
        {
            bool return_value = false;
            try
            {
                sqlConnection.Open();
                using (SQLiteCommand sqlCommand = new SQLiteCommand(CommandText, sqlConnection))
                    sqlCommand.ExecuteNonQuery();
                return_value = true;
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.ToString(), "Error"); }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close(); }
            return return_value;
        }

        public bool Delete_Record(string Condition_Name, string Condition_Value, string table_name)
        {
            bool return_value = false;
            try
            {
                sqlConnection.Open();
                using (SQLiteCommand sqlCommand = new SQLiteCommand("DELETE FROM `" + table_name + "` WHERE `" + Condition_Name + "`='" + Condition_Value + "'", sqlConnection))
                    sqlCommand.ExecuteNonQuery();
                return_value = true;
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.ToString(), "Error"); }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close(); }
            try
            {
                if (Conversions.ToDouble(Get_From_Database("SELECT COUNT(*) FROM `" + table_name + "`")) <= 0)
                {
                    sqlConnection.Open();
                    using (SQLiteCommand sqlCommand = new SQLiteCommand("TRUNCATE `" + table_name + "`", sqlConnection))
                        sqlCommand.ExecuteNonQuery();
                    return_value = true;
                }
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.ToString(), "Error"); }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close(); }
            return return_value;
        }

        public void Update_table(string Table_Name, DataGridView Table)
        {
            try
            {
                sqlConnection.Open();
                using (SQLiteDataAdapter sqlAdapter = new SQLiteDataAdapter(new SQLiteCommand("SELECT * FORM `" + Table_Name + "`", sqlConnection)))
                    sqlAdapter.Fill(sqlDataTable = new DataTable());
                Table.DataSource = new BindingSource() { DataSource = sqlDataTable };
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.ToString(), "Error"); }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close(); }
        }

        public List<string> Show_Table()
        {
            List<string> returnValue = new List<string>();
            try
            {
                sqlConnection.Open();
                using (SQLiteDataAdapter sqlAdapter = new SQLiteDataAdapter(new SQLiteCommand("SELECT table_name FROM INFORMATION_SCHEMA.tables where table_schema='DFlow DUS'", sqlConnection)))
                    sqlAdapter.Fill(sqlDataSet = new DataSet());
                foreach (DataTable table in sqlDataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                        returnValue.Add(row[0].ToString());
                }
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.ToString(), "Error"); }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close(); }
            return returnValue;
        }
        #endregion

        #region By Object
        public object getObjectFromDatabase<T>(object searching = null, bool getDataTable = false)
        {
            object return_value = null;
            try
            {
                sqlConnection.Open();
                string selectQuery = "";
                int searchingProperties = 0;
                List<T> the_array = new List<T>();
                if (searching != null)
                {
                    foreach (PropertyInfo prop in searching.GetType().GetProperties())
                    {
                        if (!((List<string>)searching.GetType().GetProperty("properties").GetValue(searching, null)).Contains(prop.Name.ToString()))
                        {
                            if (prop.GetValue(searching, null) != null || (prop.GetType() == typeof(string) && (string)prop.GetValue(searching, null) != ""))
                            {
                                if (!selectQuery.Contains(" WHERE "))
                                    selectQuery = " WHERE ";
                                selectQuery += "`" + prop.Name.ToString() + "` = '" + prop.GetValue(searching, null) + "' AND ";
                                searchingProperties += 1;
                            }
                        }
                    }
                    selectQuery = selectQuery.Substring(0, selectQuery.LastIndexOf("AND") - 1);
                }
                //MessageBox.Show("SELECT * FROM `" + Activator.CreateInstance(typeof(T)).GetType().GetProperty("database_name").GetValue(Activator.CreateInstance(typeof(T)), null).ToString() + "`" + selectQuery);
                if (searching == null || (searching != null && searchingProperties > 0))
                {
                    using (SQLiteDataAdapter sqlAdapter = new SQLiteDataAdapter(new SQLiteCommand("SELECT * FROM `" + Activator.CreateInstance(typeof(T)).GetType().GetProperty("database_name").GetValue(Activator.CreateInstance(typeof(T)), null).ToString() + "`" + selectQuery, sqlConnection)))
                        sqlAdapter.Fill(sqlDataTable = new DataTable());
                    if (getDataTable)
                    {
                        return_value = sqlDataTable;
                    }
                    else
                    {
                        using (sqlDataTable)
                        {
                            foreach (DataRow row in sqlDataTable.Rows)
                            {
                                try
                                {
                                    dynamic this_object = Activator.CreateInstance(typeof(T));
                                    anime_movie this_movie = new anime_movie();
                                    foreach (DataColumn column in row.Table.Columns)
                                    {
                                        try
                                        {
                                            if (column.DataType == typeof(string))
                                                this_object.GetType().GetProperty(column.ColumnName).SetValue(this_object, (row[column] == DBNull.Value) ? string.Empty : row[column].ToString());
                                            else
                                                this_object.GetType().GetProperty(column.ColumnName).SetValue(this_object, row[column]);
                                        }
                                        catch (Exception ex) { /*Program.Main_Form.Log(ex.Message + " at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber() + " with: " + column.ColumnName, "Error");*/ }
                                    }
                                    the_array.Add(this_object);
                                }
                                catch (Exception ex) { Program.Main_Form.Log(ex.Message + " at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), "Error"); }
                            }
                        }
                        return_value = the_array;
                    }
                }
                else
                    return_value = new List<T>() { (T)Activator.CreateInstance(typeof(T)) };
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.Message + " at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), "Error"); }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close(); }
            return return_value;
        }

        public bool insertObjectToDatabase(object inserting, ref long insertedID)
        {
            bool return_value = false;
            try
            {
                sqlConnection.Open();
                string name_string = "";
                string value_string = "";
                foreach (PropertyInfo prop in inserting.GetType().GetProperties())
                {
                    if (!((List<string>)inserting.GetType().GetProperty("properties").GetValue(inserting, null)).Contains(prop.Name.ToString()) && !((List<string>)inserting.GetType().GetProperty("AIs").GetValue(inserting, null)).Contains(prop.Name.ToString()))
                    {
                        name_string += "`" + prop.Name.ToString() + "`, ";
                        value_string += "'" + prop.GetValue(inserting, null) + "', ";
                    }
                }
                //MessageBox.Show("INSERT INTO `" + inserting.GetType().GetProperty("database_name").GetValue(inserting, null).ToString() + "` (" + name_string.Substring(0, name_string.LastIndexOf(",")) + ") VALUES(" + value_string.Substring(0, value_string.LastIndexOf(",")) + ")");
                using (SQLiteCommand sqlCommand = new SQLiteCommand("INSERT INTO `" + inserting.GetType().GetProperty("database_name").GetValue(inserting, null).ToString() + "` (" + name_string.Substring(0, name_string.LastIndexOf(",")) + ") VALUES(" + value_string.Substring(0, value_string.LastIndexOf(",")) + ")", sqlConnection))
                {
                    if (sqlCommand.ExecuteNonQuery() >= 1)
                        return_value = true;
                    else
                        return_value = false;
                }
                if (return_value)
                {
                    using (SQLiteCommand sqlCommand = new SQLiteCommand(@"select last_insert_rowid()", sqlConnection))
                        insertedID = Convert.ToInt64(sqlCommand.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                return_value = false;
                if (ex.Message.ToLower().Contains("unique constraint failed"))
                    Program.Main_Form.Log(ex.Message.Substring(ex.Message.IndexOf(":") + 2) + " at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), "Error");
                else
                    Program.Main_Form.Log(ex.Message + " at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), "Error");
            }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close(); }
            return return_value;
        }
        public bool insertObjectToDatabase(object inserting)
        {
            bool return_value = false;
            try
            {
                sqlConnection.Open();
                string name_string = "";
                string value_string = "";
                string whereString = "";
                foreach (PropertyInfo prop in inserting.GetType().GetProperties())
                {
                    if (!((List<string>)inserting.GetType().GetProperty("properties").GetValue(inserting, null)).Contains(prop.Name.ToString()) && !((List<string>)inserting.GetType().GetProperty("AIs").GetValue(inserting, null)).Contains(prop.Name.ToString()))
                    {
                        name_string += "`" + prop.Name.ToString() + "`, ";
                        value_string += "'" + prop.GetValue(inserting, null) + "', ";
                    }
                    if (prop.Name.ToString() == "Clauses")
                    {
                        List<string> clauses = (List<string>)prop.GetValue(inserting, null);
                        foreach (string clause in clauses)
                            whereString += "`" + clause + "` = '" + inserting.GetType().GetProperty(clause).GetValue(inserting, null) + "' AND ";
                    }
                }
                whereString = whereString.Substring(0, whereString.LastIndexOf("AND") - 1);
                using (SQLiteCommand sqlCommand = new SQLiteCommand("SELECT * FROM `" + inserting.GetType().GetProperty("database_name").GetValue(inserting, null).ToString() + "` WHERE " + whereString, sqlConnection))
                {
                    
                }
                //MessageBox.Show("INSERT INTO `" + inserting.GetType().GetProperty("database_name").GetValue(inserting, null).ToString() + "` (" + name_string.Substring(0, name_string.LastIndexOf(",")) + ") VALUES(" + value_string.Substring(0, value_string.LastIndexOf(",")) + ")");
                using (SQLiteCommand sqlCommand = new SQLiteCommand("INSERT INTO `" + inserting.GetType().GetProperty("database_name").GetValue(inserting, null).ToString() + "` (" + name_string.Substring(0, name_string.LastIndexOf(",")) + ") VALUES(" + value_string.Substring(0, value_string.LastIndexOf(",")) + ")", sqlConnection))
                {
                    if (sqlCommand.ExecuteNonQuery() >= 1)
                        return_value = true;
                    else
                        return_value = false;
                }
            }
            catch (Exception ex)
            {
                return_value = false;
                if (ex.Message.ToLower().Contains("unique constraint failed"))
                    Program.Main_Form.Log(ex.Message.Substring(ex.Message.IndexOf(":") + 2) + " at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), "Error");
                else
                    Program.Main_Form.Log(ex.Message + " at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), "Error");
            }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close(); }
            return return_value;
        }
        public bool updateObjectToDatabase(object updating)
        {
            bool return_value = false;
            try
            {
                sqlConnection.Open();
                string updateString = "";
                string whereString = "";
                foreach (PropertyInfo prop in updating.GetType().GetProperties())
                {
                    if (!((List<string>)updating.GetType().GetProperty("properties").GetValue(updating, null)).Contains(prop.Name.ToString()) && !((List<string>)updating.GetType().GetProperty("AIs").GetValue(updating, null)).Contains(prop.Name.ToString()))
                        updateString += "`" + prop.Name.ToString() + "` = '" + prop.GetValue(updating, null) + "', ";
                    if (prop.Name.ToString() == "Clauses")
                    {
                        List<string> clauses = (List<string>)prop.GetValue(updating, null);
                        foreach(string clause in clauses)
                            whereString += "`" + clause + "` = '" + updating.GetType().GetProperty(clause).GetValue(updating, null) + "' AND ";
                    }
                }
                updateString = updateString.Substring(0, updateString.LastIndexOf(","));
                whereString = whereString.Substring(0, whereString.LastIndexOf("AND") - 1);
                using (SQLiteCommand sqlCommand = new SQLiteCommand("UPDATE `" + updating.GetType().GetProperty("database_name").GetValue(updating, null) + "` SET " + updateString + " WHERE " + whereString, sqlConnection))
                {
                    if (sqlCommand.ExecuteNonQuery() >= 1)
                        return_value = true;
                    else
                        return_value = false;
                }
            }
            catch (Exception ex) { Program.Main_Form.Log(ex.Message + " in '" + GetType().ToString() + "' at: " + new StackTrace(ex, true).GetFrame(new StackTrace(ex, true).FrameCount - 1).GetFileLineNumber(), "Error"); }
            finally { if (sqlConnection.State == ConnectionState.Open) sqlConnection.Close(); }
            return return_value;
        }
        #endregion
    }
}