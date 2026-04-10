using System.Data;
using System.Data.Common;
using System.Collections;
using WellTool.DB.DS.Simple;

namespace WellTool.DB.Tests;

/// <summary>
/// 模拟的数据库连接用于测试
/// </summary>
public class MockDbConnection : IDbConnection
{
    public string ConnectionString { get; set; }
    public int ConnectionTimeout => 30;
    public string Database => "TestDB";
    public ConnectionState State => ConnectionState.Closed;

    public void ChangeDatabase(string databaseName) { }
    public void Close() { }
    public IDbCommand CreateCommand() => new MockDbCommand();
    public void Open() { }
    public void Dispose() { }
    public IDbTransaction BeginTransaction() => null;
    public IDbTransaction BeginTransaction(IsolationLevel il) => null;
}

/// <summary>
/// 模拟的数据库参数用于测试
/// </summary>
public class MockDbParameter : IDbDataParameter
{
    public DbType DbType { get; set; }
    public ParameterDirection Direction { get; set; }
    public bool IsNullable { get; set; }
    public string ParameterName { get; set; }
    public int Size { get; set; }
    public string SourceColumn { get; set; }
    public DataRowVersion SourceVersion { get; set; }
    public object Value { get; set; }

    public byte Precision { get; set; }
    public byte Scale { get; set; }
}

/// <summary>
/// 模拟的数据库参数集合用于测试
/// </summary>
public class MockParameterCollection : IDataParameterCollection
{
    private readonly List<IDbDataParameter> _parameters = new List<IDbDataParameter>();

    public int Add(object value) { _parameters.Add((IDbDataParameter)value); return _parameters.Count - 1; }
    public void Clear() { _parameters.Clear(); }
    public bool Contains(string parameterName) { return _parameters.Any(p => p.ParameterName == parameterName); }
    public bool Contains(object value) { return _parameters.Contains((IDbDataParameter)value); }
    public int IndexOf(string parameterName) { return _parameters.FindIndex(p => p.ParameterName == parameterName); }
    public int IndexOf(object value) { return _parameters.IndexOf((IDbDataParameter)value); }
    public void Insert(int index, object value) { _parameters.Insert(index, (IDbDataParameter)value); }
    public void Remove(object value) { _parameters.Remove((IDbDataParameter)value); }
    public void RemoveAt(string parameterName) { _parameters.RemoveAll(p => p.ParameterName == parameterName); }
    public void RemoveAt(int index) { _parameters.RemoveAt(index); }

    public object this[string parameterName] { get => _parameters.FirstOrDefault(p => p.ParameterName == parameterName); set => _parameters[IndexOf(parameterName)] = (IDbDataParameter)value; }
    public object this[int index] { get => _parameters[index]; set => _parameters[index] = (IDbDataParameter)value; }

    public int Count => _parameters.Count;
    public bool IsFixedSize => false;
    public bool IsReadOnly => false;
    public bool IsSynchronized => false;
    public object SyncRoot => this;

    public void CopyTo(Array array, int index) { _parameters.CopyTo((IDbDataParameter[])array, index); }
    public IEnumerator GetEnumerator() => _parameters.GetEnumerator();
}

/// <summary>
/// 模拟的数据库命令用于测试
/// </summary>
public class MockDbCommand : IDbCommand
{
    public string CommandText { get; set; }
    public int CommandTimeout { get; set; }
    public CommandType CommandType { get; set; }
    public IDbConnection Connection { get; set; }
    public IDataParameterCollection Parameters { get; } = new MockParameterCollection();
    public IDbTransaction Transaction { get; set; }
    public UpdateRowSource UpdatedRowSource { get; set; }

    public void Cancel() { }
    public IDbDataParameter CreateParameter() => new MockDbParameter();
    public int ExecuteNonQuery() => 0;
    public IDataReader ExecuteReader() => new MockDataReader(new string[0], new object[0]);
    public IDataReader ExecuteReader(CommandBehavior behavior) => new MockDataReader(new string[0], new object[0]);
    public object ExecuteScalar() => null;
    public void Prepare() { }
    public void Dispose() { }
}

/// <summary>
/// 模拟的 DataReader 用于测试
/// </summary>
public class MockDataReader : DbDataReader
{
    private bool _hasRead = false;
    private readonly object[] _values;
    private readonly string[] _columnNames;

    public MockDataReader(string[] columnNames, object[] values)
    {
        _columnNames = columnNames;
        _values = values;
    }

    public override bool Read()
    {
        if (_hasRead)
            return false;
        _hasRead = true;
        return true;
    }

    public override object GetValue(int ordinal)
    {
        return _values[ordinal];
    }

    public override string GetName(int ordinal)
    {
        return _columnNames[ordinal];
    }

    public override int FieldCount => _columnNames.Length;

    // 其他必要的抽象方法实现
    public override bool IsDBNull(int ordinal) => _values[ordinal] == DBNull.Value;
    public override int Depth => 0;
    public override bool IsClosed => false;
    public override int RecordsAffected => 0;
    public override void Close() { }
    public override DataTable GetSchemaTable() => null;
    public override bool NextResult() => false;
    public override int GetOrdinal(string name) => Array.IndexOf(_columnNames, name);
    public override bool GetBoolean(int ordinal) => (bool)_values[ordinal];
    public override byte GetByte(int ordinal) => (byte)_values[ordinal];
    public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length) => 0;
    public override char GetChar(int ordinal) => (char)_values[ordinal];
    public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length) => 0;
    public override string GetDataTypeName(int ordinal) => _values[ordinal].GetType().Name;
    public override DateTime GetDateTime(int ordinal) => (DateTime)_values[ordinal];
    public override decimal GetDecimal(int ordinal) => (decimal)_values[ordinal];
    public override double GetDouble(int ordinal) => (double)_values[ordinal];
    public override Type GetFieldType(int ordinal) => _values[ordinal].GetType();
    public override float GetFloat(int ordinal) => (float)_values[ordinal];
    public override Guid GetGuid(int ordinal) => (Guid)_values[ordinal];
    public override short GetInt16(int ordinal) => (short)_values[ordinal];
    public override int GetInt32(int ordinal) => (int)_values[ordinal];
    public override long GetInt64(int ordinal) => (long)_values[ordinal];
    public override string GetString(int ordinal) => (string)_values[ordinal];
    public override int GetValues(object[] values) => 0;
    public override object this[string name] => _values[Array.IndexOf(_columnNames, name)];
    public override object this[int ordinal] => _values[ordinal];
    public override IEnumerator GetEnumerator() => null;
    public override bool HasRows => true;
}

/// <summary>
/// 测试用的数据源实现，实现了IDbDataSource接口
/// </summary>
public class TestDataSource : IDbDataSource
{
    private readonly string _connectionString;
    private readonly string _driverClass;

    public TestDataSource(string connectionString, string driverClass)
    {
        _connectionString = connectionString;
        _driverClass = driverClass;
    }

    public IDbConnection GetConnection()
    {
        var connection = new MockDbConnection();
        connection.ConnectionString = _connectionString;
        return connection;
    }

    public IDbConnection GetConnection(string connectionString)
    {
        var connection = new MockDbConnection();
        connection.ConnectionString = connectionString;
        return connection;
    }
}

/// <summary>
/// 测试用的数据库实现
/// </summary>
public class TestDb : AbstractDb
{
    public TestDb(IDbDataSource ds) : base(ds) { }

    public override IDbConnection GetConnection()
    {
        return _ds.GetConnection();
    }

    public override void CloseConnection(IDbConnection conn)
    {
        conn?.Close();
    }
}
