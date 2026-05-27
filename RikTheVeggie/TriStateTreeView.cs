using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RikTheVeggie;

public class TriStateTreeView : TreeView
{
	public enum CheckedState
	{
		UnInitialised = -1,
		UnChecked,
		Checked,
		Mixed
	}

	public enum TriStateStyles
	{
		Standard,
		Installer
	}

	private int IgnoreClickAction = 0;

	private TriStateStyles TriStateStyle = TriStateStyles.Standard;

	[Category("Tri-State Tree View")]
	[DisplayName("Style")]
	[Description("Style of the Tri-State Tree View")]
	public TriStateStyles TriStateStyleProperty
	{
		get
		{
			return TriStateStyle;
		}
		set
		{
			TriStateStyle = value;
		}
	}

	public TriStateTreeView()
	{
		base.StateImageList = new ImageList();
		for (int i = 0; i < 3; i++)
		{
			Bitmap bmp = new Bitmap(16, 16);
			Graphics chkGraphics = Graphics.FromImage(bmp);
			switch (i)
			{
			case 0:
				CheckBoxRenderer.DrawCheckBox(chkGraphics, new Point(0, 1), CheckBoxState.UncheckedNormal);
				break;
			case 1:
				CheckBoxRenderer.DrawCheckBox(chkGraphics, new Point(0, 1), CheckBoxState.CheckedNormal);
				break;
			case 2:
				CheckBoxRenderer.DrawCheckBox(chkGraphics, new Point(0, 1), CheckBoxState.MixedNormal);
				break;
			}
			base.StateImageList.Images.Add(bmp);
		}
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();
		base.CheckBoxes = false;
		IgnoreClickAction++;
		UpdateChildState(base.Nodes, 0, Checked: false, ChangeUninitialisedNodesOnly: true);
		IgnoreClickAction--;
	}

	protected override void OnAfterCheck(TreeViewEventArgs e)
	{
		base.OnAfterCheck(e);
		if (IgnoreClickAction <= 0)
		{
			IgnoreClickAction++;
			TreeNode tn = e.Node;
			tn.StateImageIndex = (tn.Checked ? 1 : 0);
			UpdateChildState(e.Node.Nodes, e.Node.StateImageIndex, e.Node.Checked, ChangeUninitialisedNodesOnly: false);
			UpdateParentState(e.Node.Parent);
			IgnoreClickAction--;
		}
	}

	protected override void OnAfterExpand(TreeViewEventArgs e)
	{
		base.OnAfterExpand(e);
		IgnoreClickAction++;
		UpdateChildState(e.Node.Nodes, e.Node.StateImageIndex, e.Node.Checked, ChangeUninitialisedNodesOnly: true);
		IgnoreClickAction--;
	}

	protected void UpdateChildState(TreeNodeCollection Nodes, int StateImageIndex, bool Checked, bool ChangeUninitialisedNodesOnly)
	{
		foreach (TreeNode tnChild in Nodes)
		{
			if (!ChangeUninitialisedNodesOnly || tnChild.StateImageIndex == -1)
			{
				tnChild.StateImageIndex = StateImageIndex;
				tnChild.Checked = Checked;
				if (tnChild.Nodes.Count > 0)
				{
					UpdateChildState(tnChild.Nodes, StateImageIndex, Checked, ChangeUninitialisedNodesOnly);
				}
			}
		}
	}

	protected void UpdateParentState(TreeNode tn)
	{
		if (tn == null)
		{
			return;
		}
		int OrigStateImageIndex = tn.StateImageIndex;
		int UnCheckedNodes = 0;
		int CheckedNodes = 0;
		int MixedNodes = 0;
		foreach (TreeNode tnChild in tn.Nodes)
		{
			if (tnChild.StateImageIndex == 1)
			{
				CheckedNodes++;
				continue;
			}
			if (tnChild.StateImageIndex == 2)
			{
				MixedNodes++;
				break;
			}
			UnCheckedNodes++;
		}
		if (TriStateStyle == TriStateStyles.Installer && MixedNodes == 0)
		{
			if (UnCheckedNodes == 0)
			{
				tn.Checked = true;
			}
			else
			{
				tn.Checked = false;
			}
		}
		if (MixedNodes > 0)
		{
			tn.StateImageIndex = 2;
		}
		else if (CheckedNodes > 0 && UnCheckedNodes == 0)
		{
			if (tn.Checked)
			{
				tn.StateImageIndex = 1;
			}
			else
			{
				tn.StateImageIndex = 2;
			}
		}
		else if (CheckedNodes > 0)
		{
			tn.StateImageIndex = 2;
		}
		else if (tn.Checked)
		{
			tn.StateImageIndex = 2;
		}
		else
		{
			tn.StateImageIndex = 0;
		}
		if (OrigStateImageIndex != tn.StateImageIndex && tn.Parent != null)
		{
			UpdateParentState(tn.Parent);
		}
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (e.KeyCode == Keys.Space)
		{
			base.SelectedNode.Checked = !base.SelectedNode.Checked;
		}
	}

	protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
	{
		base.OnNodeMouseClick(e);
		TreeViewHitTestInfo info = HitTest(e.X, e.Y);
		if (info != null && info.Location == TreeViewHitTestLocations.StateImage)
		{
			TreeNode tn = e.Node;
			tn.Checked = !tn.Checked;
		}
	}
}
