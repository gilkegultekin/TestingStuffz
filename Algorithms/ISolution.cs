namespace Algorithms
{
    public interface ISolution<out TResult>
    {
        TResult Solve();
    }

    public interface ISolution<out TResult, in TParameter>
    {
        TResult Solve(TParameter param);
    }

    public interface ISolution<out TResult, in TParameter1, TParameter2>
    {
        TResult Solve(TParameter1 param1, TParameter2 param2);
    }
}