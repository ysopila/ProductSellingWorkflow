namespace ProductSellingWorkflow.Service.Events
{
	public class PropertyChangeEvent: EventBase
	{
		public PropertyChangeEvent(object value)
		{
			Value = value;
		}

		internal object Value { get; }
	}

	public class PropertyChangeEvent<T> : PropertyChangeEvent
	{
		public PropertyChangeEvent(T value): base(value)
		{
		}

		internal new T Value => (T)base.Value;
	}
}