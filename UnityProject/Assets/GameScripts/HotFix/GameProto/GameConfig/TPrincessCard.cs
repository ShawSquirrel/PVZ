
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
public partial class TPrincessCard
{
    private readonly System.Collections.Generic.Dictionary<EPrincessType, VPrincessCard> _dataMap;
    private readonly System.Collections.Generic.List<VPrincessCard> _dataList;
    
    public TPrincessCard(ByteBuf _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<EPrincessType, VPrincessCard>();
        _dataList = new System.Collections.Generic.List<VPrincessCard>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            VPrincessCard _v;
            _v = VPrincessCard.DeserializeVPrincessCard(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.PrincessType, _v);
        }
    }

    public System.Collections.Generic.Dictionary<EPrincessType, VPrincessCard> DataMap => _dataMap;
    public System.Collections.Generic.List<VPrincessCard> DataList => _dataList;

    public VPrincessCard GetOrDefault(EPrincessType key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public VPrincessCard Get(EPrincessType key) => _dataMap[key];
    public VPrincessCard this[EPrincessType key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}

