using System;
using Manufacturing.Framework.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manufacturing.Framework.Datasource
{
    [TestClass]
    public class DatasourceRecordExtensions
    {
        [TestMethod]
        public void GetSetDecimalValue()
        {
            var dr = new DatasourceRecord();
            dr.SetDecimalValue(123456789M);
            var dv = dr.GetDecimalValue();
            Assert.AreEqual(123456789M, dv);
            Assert.AreEqual(DatasourceRecord.DataTypeEnum.Decimal, dr.DataType);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDatasourceRecordConversion))]
        public void SetDecimalValue_DontAllowTypeToChange()
        {
            var dr = new DatasourceRecord();
            dr.SetDoubleValue(123);
            dr.SetDecimalValue(123M);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDatasourceRecordConversion))]
        public void GetDecimalValue_DontAllowTypeToChange()
        {
            var dr = new DatasourceRecord();
            dr.SetDoubleValue(123);
            dr.GetDecimalValue();
        }

        [TestMethod]
        public void GetSetDoubleValue()
        {
            var dr = new DatasourceRecord();
            dr.SetDoubleValue(4.5);
            Assert.AreEqual(4.5, dr.GetDoubleValue());
            Assert.AreEqual(DatasourceRecord.DataTypeEnum.Double, dr.DataType);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDatasourceRecordConversion))]
        public void SetDoubleValue_DontAllowTypeToChange()
        {
            var dr = new DatasourceRecord();
            dr.SetIntValue(123);
            dr.SetDoubleValue(1234);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDatasourceRecordConversion))]
        public void GetDoubleValue_DontAllowTypeToChange()
        {
            var dr = new DatasourceRecord();
            dr.SetIntValue(123);
            dr.GetDoubleValue();
        }

        [TestMethod]
        public void GetSetIntValue()
        {
            var dr = new DatasourceRecord();
            dr.SetIntValue(4);
            Assert.AreEqual(4, dr.GetIntValue());
            Assert.AreEqual(DatasourceRecord.DataTypeEnum.Integer, dr.DataType);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDatasourceRecordConversion))]
        public void SetIntValue_DontAllowTypeToChange()
        {
            var dr = new DatasourceRecord();
            dr.SetDoubleValue(123);
            dr.SetIntValue(1234);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDatasourceRecordConversion))]
        public void GetIntValue_DontAllowTypeToChange()
        {
            var dr = new DatasourceRecord();
            dr.SetDoubleValue(123);
            dr.GetIntValue();
        }

        [TestMethod]
        public void GetSetStringValue()
        {
            var dr = new DatasourceRecord();
            dr.SetStringValue("hi there");
            Assert.AreEqual("hi there", dr.GetStringValue());
            Assert.AreEqual(DatasourceRecord.DataTypeEnum.String, dr.DataType);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDatasourceRecordConversion))]
        public void SetStringValue_DontAllowTypeToChange()
        {
            var dr = new DatasourceRecord();
            dr.SetDoubleValue(123);
            dr.SetStringValue("hi");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDatasourceRecordConversion))]
        public void GetStringValue_DontAllowTypeToChange()
        {
            var dr = new DatasourceRecord();
            dr.SetDoubleValue(123);
            dr.GetStringValue();
        }
    }
}
