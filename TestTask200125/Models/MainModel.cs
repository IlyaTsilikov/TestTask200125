using Spire.Xls;
using Spire.Xls.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask200125.Models
{
    public class MainModel
    {
        private ObjectInfo[] _objects;

        public IEnumerable<ObjectInfo> Objects
        {
            get { return _objects; }
        }

        public async Task ImportXlsAsync(string fileName)
        {
            await Task.Run(() => ImportXls(fileName));
        }

        public void ImportXls(string fileName)
        {
            Workbook xls = new Workbook();
            xls.LoadFromFile(fileName);
            System.Diagnostics.Debug.WriteLine(xls.Worksheets.FirstOrDefault().Rows[0]);
            _objects = xls.Worksheets.FirstOrDefault().Rows.Skip(1).Select(row => ImportRow(row)).Where(info => info != null).ToArray();
        }

        private ObjectInfo ImportRow(IXLSRange row)
        {
            if (row.CellList.Count != ObjectInfo.ColumnsCount)
                return null;
            return new ObjectInfo
            {
                Angle = StringToDouble(row.CellList[ObjectInfo.AngleIndex].Value),
                Distance = StringToDouble(row.CellList[ObjectInfo.DistanceIndex].Value),
                Hegth = StringToDouble(row.CellList[ObjectInfo.HegthIndex].Value),
                IsDefect = StringToBool(row.CellList[ObjectInfo.IsDefectIndex].Text),
                Name = row.CellList[ObjectInfo.NameIndex].Text,
                Width = StringToDouble(row.CellList[ObjectInfo.WidthIndex].Value),
            };
        }

        private double? StringToDouble(string strValue)
        {
            double result;
            if (!double.TryParse(strValue, out result))
                return null;
            return result;
        }

        private bool? StringToBool(string text)
        {
            switch (text?.ToLowerInvariant())
            {
                case ObjectInfo.TrueValueName:
                    return true;
                case ObjectInfo.FalseValueName:
                    return false;
                default:
                    return null;
            }
        }
    }
}
