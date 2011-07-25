﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vixen.Common;
using Vixen.Sys;
using CommandStandard;

namespace Vixen.Module.Effect {
    public interface IEffect {
		///// <summary>
		///// Set when parameters or time span have been modified.
		///// </summary>
		//bool IsDirty { get; set; }
		/// <summary>
		/// "You're not executing yet, but render as much as you can."
		/// </summary>
		void PreRender(ChannelNode[] nodes, long timeSpan, object[] parameterValues);
		/// <summary>
		/// "You're executing, give me everything."
		/// </summary>
		/// <param name="nodes">Nodes the effect is being applied to as a single collection.</param>
		/// <param name="timeSpan">The length of the entire effect.</param>
		/// <param name="renderStartTime">Start time of the desired data relative to the start of the effect.</param>
		/// <param name="renderTimeSpan">Amount of time to render.</param>
		/// <param name="parameterValues">Effect parameter values.</param>
		/// <returns></returns>
		ChannelData Render(ChannelNode[] nodes, long timeSpan, object[] parameterValues);
		string EffectName { get; }
		CommandParameterSpecification[] Parameters { get; }
    }
}