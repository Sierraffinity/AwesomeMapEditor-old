using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using Nintenlord.Collections;
using Nintenlord.Utility;

namespace Nintenlord.ROMHacking
{
    /// <summary>
    /// Contains data of a file, both general and application specific.
    /// </summary>
    [Serializable]
    public class NLZFile : IXmlSerializable, ISerializable
    {
        int crc32;
        int fileSize;
        IDataChange<byte> identificationData;
        IIndexOverlay freeSpace;
        Dictionary<string, object> appData;

        private const string crc32Name = "CRC32";
        private const string fileSizeName = "File size";
        private const string identificationDataName = "Identification data";
        private const string freeSpaceName = "Free space";
        private const string appDataName = "App data";

        /// <summary>
        /// Gets or sets the CRC32 of the file.
        /// </summary>
        public int CRC32
        {
            get { return crc32; }
            set { crc32 = value; }
        }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        public int FileSize
        {
            get { return fileSize; }
            set { fileSize = value; }
        }

        /// <summary>
        /// Consrtucts a new NLZFile.
        /// <remarks>
        /// CRC32 == 0 &&
        /// FileSize == 0 &&
        /// forall int i: IsSpaceFree(i, 1) == false &&
        /// forall String s: ContainsAppData(s) == false
        /// </remarks>
        /// </summary>
        public NLZFile()
        {
            appData = new Dictionary<string, object>();
            freeSpace = new IndexOverlay();
            identificationData = new DataChange<byte>();
            crc32 = 0;
            fileSize = 0;
        }

        private NLZFile(SerializationInfo information, StreamingContext context)
        {
            crc32 = information.GetInt32(crc32Name);
            fileSize = information.GetInt32(fileSizeName);

            identificationData = new DataChange<byte>();            
            var idData = information.GetValue<KeyValuePair<int, byte[]>[]>(identificationDataName);
            foreach (var item in idData)
            {
                identificationData.AddChangedData(item.Key, item.Value);
            }

            freeSpace = new IndexOverlay();
            var fSpace = information.GetValue<KeyValuePair<int, int>[]>(freeSpaceName);
            foreach (var item in fSpace)
            {
                freeSpace.AddIndexes(item.Key, item.Value);
            }

            appData = new Dictionary<string, object>();
            var aData = information.GetValue<KeyValuePair<string, object>[]>(appDataName);
            appData.AddAll(aData);
        }

        #region File data

        public void AddAppData<T>(string appName, T data)
        {
            if (!data.GetType().IsSerializable)
            {
                throw new ArgumentException("Parameter needs to be serializable.", "data");
            }

            appData[appName] = data;
        }

        public bool ContainsAppData(string appName)
        {
            return appData.ContainsKey(appName);
        }

        public bool RemoveAppData(string appName)
        {
            return appData.Remove(appName);
        }

        public T GetAppData<T>(string appName)
        {
            object obj = appData[appName];
            if (obj is T)
            {
                return (T)obj;
            }
            else
            {
                throw new InvalidCastException(
                    "Type of appName's setting not compatible with "
                    + typeof(T));
            }
        }


        public void AddFreeSpace(int offset, int amount)
        {
            freeSpace.AddIndexes(offset, amount);
        }

        public void RemoveFreeSpace(int offset, int amount)
        {
            freeSpace.RemoveIndexes(offset, amount);
        }

        public bool IsSpaceFree(int offset, int amount)
        {
            return freeSpace.ContainsAllIndexes(offset, amount);
        }
        
        #endregion

        /// <summary>
        /// Finds a block of free data of size.
        /// </summary>
        /// <param name="size">Size of requested block of free space.</param>
        /// <returns>Offset of free data or -1 if no free space öarge enough was found.</returns>
        public int GetFreeData(int size)
        {
            KeyValuePair<int, int>[] array = freeSpace.ToArray<KeyValuePair<int, int>>();
            if (array.Length == 0)
                return -1;
            List<KeyValuePair<int, int>> values = new List<KeyValuePair<int,int>>();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Value >= size)
                {
                    values.Add(array[i]);
                }
            }
            if (values.Count > 0)
            {
                values.Sort((x, y) => x.Value - y.Value);
                return values[0].Key;
            }
            return -1;
        }
        
        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(crc32Name, crc32);
            info.AddValue(fileSizeName, fileSize);
            info.AddValue(identificationDataName, 
                (identificationData as IEnumerable<KeyValuePair<int,byte[]>>).ToArray(), 
                typeof(KeyValuePair<int, byte[]>[]));
            info.AddValue(freeSpaceName, 
                (freeSpace as IEnumerable<KeyValuePair<int,int>>).ToArray(), 
                typeof(KeyValuePair<int, int>[]));
            info.AddValue(appDataName, appData.ToArray(),
                typeof(KeyValuePair<string, object>[]));
        }

        #endregion

        #region IXmlSerializable Members

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
