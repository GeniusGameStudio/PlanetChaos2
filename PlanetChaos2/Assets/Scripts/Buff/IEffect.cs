/// <summary>
/// 可造成影响的接口
/// </summary>
public interface IEffect
{
    /// <summary>
    /// 产生影响
    /// </summary>
    void Effect();

    /// <summary>
    /// 终止影响
    /// </summary>
    void EndEffect();
}
