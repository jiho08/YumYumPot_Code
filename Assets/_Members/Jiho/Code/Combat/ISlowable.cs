namespace Code.Combat
{
    public interface ISlowable
    {
        void ApplySlow(float slowRatio, float duration);
    }
}