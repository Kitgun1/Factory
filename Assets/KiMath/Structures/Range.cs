namespace Factory
{
	public struct IntRange
	{
        public IntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; private set; }
        public int Max { get; private set; }
	}

    public struct FloatRange
    {
        public FloatRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Min { get; private set; }
        public float Max { get; private set; }
    }
}