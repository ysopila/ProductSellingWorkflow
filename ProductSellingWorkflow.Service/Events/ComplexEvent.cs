using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.Events
{
	public abstract class ComplexEvent : EventBase
	{
		protected internal IDictionary<string, PropertyChangeEvent> Events { get; } = new Dictionary<string, PropertyChangeEvent>();

		protected void SetEvent(string key, PropertyChangeEvent e)
		{
			if (Events.ContainsKey(key))
				Events[key] = e;
			else
				Events.Add(key, e);
		}

		protected T GetEvent<T>(string key) where T: PropertyChangeEvent
		{
			if (Events.ContainsKey(key))
				return (T)Events[key];
			else
				return null;
		}

		protected IEnumerable<T> GetEventCollection<T>(string key) where T : PropertyChangeEvent
		{
			return Events.Keys.Where(x => x.StartsWith(key)).Select(x => GetEvent<T>(x));
		}

		protected void SetEventCollection(string key, IEnumerable<PropertyChangeEvent> e)
		{
			foreach (var x in Events.Keys.Where(x => x.StartsWith(key)))
				Events.Remove(x);
			if (e != null && e.Any())
			{
				var array = e.ToArray();
				for (int i = 0; i < array.Length; i++)
					SetEvent($"{key}[{i}]", array[i]);
			}
		}
	}
}