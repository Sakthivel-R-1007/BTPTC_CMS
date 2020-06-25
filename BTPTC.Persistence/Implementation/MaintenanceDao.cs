using BTPTC.Domain;
using BTPTC.Persistence.DBConnectionFactory;
using BTPTC.Persistence.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Persistence.Implementation
{
    public class MaintenanceDao : IMaintenanceDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public MaintenanceDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion

        public Int64 SaveExcelLog(string FileGUID, string FileName, string SystemIp, Guid UserId)
        {
            Int64 ExcelId = 0;
            DynamicParameters param = new DynamicParameters();
            param.Add("@FileGuid", FileGUID, dbType: DbType.String);
            param.Add("@FileName", FileName, dbType: DbType.String);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", UserId, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveMaintenanceExcel]";
                ExcelId = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return ExcelId;
        }

        public int SaveMaintenance(IList<MaintenanceSchedule> MS, Int64 ExcelId, string SystemIp, Guid UserId)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();
            param.Add("@ExcelId", ExcelId, dbType: DbType.Int64);
            param.Add("@SystemIP", SystemIp, dbType: DbType.String);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@MaintenanceTbl", GenerateDT(MS).AsTableValuedParameter());

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveMaintenance]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
            }
            return result;
        }


        private DataTable GenerateDT(IList<MaintenanceSchedule> maintenances)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Division", typeof(string));
            dt.Columns.Add("BlkNo", typeof(string));
            dt.Columns.Add("StreetName", typeof(string));
            dt.Columns.Add("RCZone", typeof(string));
            dt.Columns.Add("BlockWashing", typeof(string));
            dt.Columns.Add("BinChuteFogging", typeof(string));
            dt.Columns.Add("BinChuteFlushing", typeof(string));
            dt.Columns.Add("LiftMaintenance", typeof(string));


            if (maintenances != null && maintenances.Count > 0)
            {
                int i = 0;
                foreach (MaintenanceSchedule _TS in maintenances)
                {
                    i++;
                    DataRow dr = dt.NewRow();
                    dr["Division"] = _TS.Division;
                    dr["BlkNo"] = _TS.BlkNo;
                    dr["StreetName"] = _TS.StreetName;
                    dr["RCZone"] = _TS.RCZone;
                    dr["BlockWashing"] = _TS.BlockWashing;
                    dr["BinChuteFogging"] = _TS.BinChuteFogging;
                    dr["BinChuteFlushing"] = _TS.BinChuteFlushing;
                    dr["LiftMaintenance"] = _TS.LiftMaintenance;

                    dt.Rows.Add(dr);


                }
            }
            return dt;
        }

        public IList<MaintenanceSchedule> GetStreetList()
        {
            IList<MaintenanceSchedule> maintenances = null;
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetMaintenanceStreetNameList]";
                maintenances = conn.Query<MaintenanceSchedule>(SQL, commandType: CommandType.StoredProcedure).ToList<MaintenanceSchedule>();
            }
            return maintenances;

        }

        public IList<MaintenanceSchedule> GetMaintenanceScheduleListByStreet(string StreetName, string BlkNo)
        {
            IList<MaintenanceSchedule> maintenances = null;
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetMaintenanceScheduleList]";
                DynamicParameters param = new DynamicParameters();
                param.Add("@StreetName", StreetName, dbType: DbType.String);
                param.Add("@BlkNo", BlkNo, dbType: DbType.String);
                maintenances = conn.Query<MaintenanceSchedule>(SQL, param, commandType: CommandType.StoredProcedure).ToList<MaintenanceSchedule>();
            }
            return maintenances;

        }

        public List<MaintenanceSchedule> GetViewMaintenance()
        {
            List<MaintenanceSchedule> maintenanceSchedule = new List<MaintenanceSchedule>();
            DynamicParameters param = new DynamicParameters();

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetViewMaintenance]";
                maintenanceSchedule = conn.Query<MaintenanceSchedule>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return maintenanceSchedule;
        }
    }
}
