/// <summary>
/// 可被治愈的接口
/// </summary>
public interface IHeal
{
    /// <summary>
    /// 被治愈healPoint点生命值
    /// </summary>
    /// <param name="healPoint"></param>
    void Heal(int healPoint);
}
