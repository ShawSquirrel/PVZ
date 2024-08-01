
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;


namespace GameConfig
{
public partial class TAnim
{
    private readonly System.Collections.Generic.Dictionary<int, VAnim> _dataMap;
    private readonly System.Collections.Generic.List<VAnim> _dataList;
    
    public TAnim(ByteBuf _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, VAnim>();
        _dataList = new System.Collections.Generic.List<VAnim>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            VAnim _v;
            _v = VAnim.DeserializeVAnim(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, VAnim> DataMap => _dataMap;
    public System.Collections.Generic.List<VAnim> DataList => _dataList;

    public VAnim GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public VAnim Get(int key) => _dataMap[key];
    public VAnim this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}
