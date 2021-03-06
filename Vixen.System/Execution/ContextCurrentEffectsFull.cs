﻿using System;
using System.Collections.Generic;
using System.Linq;
using Vixen.Execution.DataSource;
using Vixen.Sys;

namespace Vixen.Execution
{
	/// <summary>
	/// Maintains a list of current effects for a context.
	/// The IDataSource is expected to provide every qualifying effect, not just newly qualified effects.
	/// </summary>
	internal class ContextCurrentEffectsFull : IContextCurrentEffects
	{
		private List<IEffectNode> _currentEffects;
		private HashSet<Guid> _currentAffectedElements;

		public ContextCurrentEffectsFull()
		{
			_currentEffects = new List<IEffectNode>();
			_currentAffectedElements = new HashSet<Guid>();
		}

		/// <summary>
		/// Updates the collection of current affects, returning the ids of the affected elements.
		/// </summary>
		/// <returns>Ids of the affected elements.</returns>
		public Guid[] UpdateCurrentEffects(IDataSource dataSource, TimeSpan currentTime)
		{
			// Get the entirety of the new state.
			IEffectNode[] newState = dataSource.GetDataAt(currentTime).ToArray();
			// Get the elements affected by this new state.
			Guid[] nowAffectedElements = _GetAffectedElements(newState).ToArray();
			// New and expiring effects affect the state, so get the union of
			// the previous state and the current state.
			//HashSet<Guid> allAffectedElements = new HashSet<Guid>(_currentAffectedElements.Concat(newAffectedElements));
			IEnumerable<Guid> allAffectedElements = _currentAffectedElements.Concat(nowAffectedElements).Distinct();
			// Set the new state.
			_currentEffects = new List<IEffectNode>(newState);
			_currentAffectedElements = new HashSet<Guid>(nowAffectedElements);

			return allAffectedElements.ToArray();
		}

		public void Reset()
		{
			_currentEffects.Clear();
		}

		private IEnumerable<Guid> _GetAffectedElements(IEnumerable<IEffectNode> effectNodes)
		{
			return effectNodes.SelectMany(x => x.Effect.TargetNodes).SelectMany(y => y.GetElementEnumerator()).Select(z => z.Id);
		}

		public IEnumerator<IEffectNode> GetEnumerator()
		{
			return _currentEffects.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}