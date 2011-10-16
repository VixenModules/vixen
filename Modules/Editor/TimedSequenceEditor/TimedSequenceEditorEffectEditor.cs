﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vixen.Sys;
using Vixen.Module.Effect;
using Vixen.Module.EffectEditor;

namespace VixenModules.Editor.TimedSequenceEditor
{
	public partial class TimedSequenceEditorEffectEditor : Form
	{
		private EffectNode _effectNode;
		private IEffectEditorControl _control;

		public TimedSequenceEditorEffectEditor(EffectNode effectNode)
		{
			InitializeComponent();

			//*** GetEffectEditorControls now returns a collection of controls.
			//    There needs to be some effort to break up the effect's parameters
			//    among the controls.
			_effectNode = effectNode;
			_control = ApplicationServices.GetEffectEditorControls(_effectNode.Effect.Descriptor.TypeId).First();
			_control.EffectParameterValues = _effectNode.Effect.ParameterValues;
			panelEditorControls.Controls.Add(_control as Control);
		}

		private void TimedSequenceEditorEffectEditor_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == System.Windows.Forms.DialogResult.OK) {
				_effectNode.Effect.ParameterValues = _control.EffectParameterValues;
			}
		}
	}
}