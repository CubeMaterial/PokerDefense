using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using _ShaderoShaderEditorFramework;
using _ShaderoShaderEditorFramework.Utilities;

namespace _ShaderoShaderEditorFramework 
{
	public static class NodeEditorInputControls
	{
		#region Canvas Context Entries

		[ContextFillerAttribute (ContextType.Canvas)]
		private static void FillAddNodes (NodeEditorInputInfo inputInfo, GenericMenu canvasContextMenu) 
		{	NodeEditorState state = inputInfo.editorState;
			List<Node> displayedNodes = state.connectOutput != null? NodeTypes.getCompatibleNodes (state.connectOutput) : NodeTypes.nodes.Keys.ToList ();
            foreach (Node compatibleNode in displayedNodes)
			{
               
                if (compatibleNode.GetType().Name == "BuildShader" && NodeEditor.NoBuildShaderContext) continue;
                if (NodeCanvasManager.CheckCanvasCompability (compatibleNode, inputInfo.editorState.canvas.GetType ()))
					canvasContextMenu.AddItem (new GUIContent ("" + NodeTypes.nodes[compatibleNode].adress), false, CreateNodeCallback, new NodeEditorInputInfo (compatibleNode.GetID, state));
                
            }
		}

		private static void CreateNodeCallback (object infoObj)
		{
			NodeEditorInputInfo callback = infoObj as NodeEditorInputInfo;
			if (callback == null)
				throw new UnityException ("Callback Object passed by context is not of type NodeEditorInputInfo!");
      		callback.SetAsCurrentEnvironment ();
			Node.Create (callback.message, NodeEditor.ScreenToCanvasSpace (callback.inputPos), callback.editorState.connectOutput);
			callback.editorState.connectOutput = null;
			NodeEditor.RepaintClients ();
        }

		#endregion

		#region Node Context Entries
     
		[ContextEntryAttribute (ContextType.Node, "Delete Node")]
		private static void DeleteNode (NodeEditorInputInfo inputInfo) 
		{
			inputInfo.SetAsCurrentEnvironment ();
			if (inputInfo.editorState.focusedNode != null) 
			{
				inputInfo.editorState.focusedNode.Delete ();
				inputInfo.inputEvent.Use ();
			}
        }

		[ContextEntryAttribute (ContextType.Node, "Duplicate Node")]
		private static void DuplicateNode (NodeEditorInputInfo inputInfo) 
		{
            inputInfo.SetAsCurrentEnvironment ();
			NodeEditorState state = inputInfo.editorState;
			if (state.focusedNode != null) 
			{ 
				Node duplicatedNode = Node.Create (state.focusedNode.GetID, NodeEditor.ScreenToCanvasSpace (inputInfo.inputPos), state.connectOutput);
				state.selectedNode = state.focusedNode = duplicatedNode;
				state.connectOutput = null;
				inputInfo.inputEvent.Use ();
			}
		}

        #endregion

        #region Node Keyboard Control

      

		#endregion

		#region Node Dragging

		[EventHandlerAttribute (EventType.MouseDown, 110)] 
		private static void HandleNodeDraggingStart (NodeEditorInputInfo inputInfo) 
		{
			if (GUIUtility.hotControl > 0)
				return; 

			NodeEditorState state = inputInfo.editorState;
			if (inputInfo.inputEvent.button == 0 && state.focusedNode != null && state.focusedNode == state.selectedNode && state.focusedNodeKnob == null) 
			{
				state.dragNode = true;
				state.dragStart = inputInfo.inputPos;
				state.dragPos = state.focusedNode.rect.position; 
				state.dragOffset = Vector2.zero;
				inputInfo.inputEvent.delta = Vector2.zero;
			}
		}

		[EventHandlerAttribute (EventType.MouseDrag)]
		private static void HandleNodeDragging (NodeEditorInputInfo inputInfo) 
		{
			NodeEditorState state = inputInfo.editorState;
			if (state.dragNode) 
			{ 
				if (state.selectedNode != null && GUIUtility.hotControl == 0)
				{ 
					state.dragOffset = inputInfo.inputPos-state.dragStart;
					state.selectedNode.rect.position = state.dragPos + state.dragOffset*state.zoom;
					NodeEditorCallbacks.IssueOnMoveNode (state.selectedNode);
					NodeEditor.RepaintClients ();
				} 
				else
					state.dragNode = false;
			}
		}

		[EventHandlerAttribute (EventType.MouseDown)]
		[EventHandlerAttribute (EventType.MouseUp)]
		private static void HandleNodeDraggingEnd (NodeEditorInputInfo inputInfo) 
		{
			inputInfo.editorState.dragNode = false;
		}

		#endregion

		#region Window Panning

		[EventHandlerAttribute (EventType.MouseDown, 100)] 
		private static void HandleWindowPanningStart (NodeEditorInputInfo inputInfo) 
		{
			if (GUIUtility.hotControl > 0)
				return; 

			NodeEditorState state = inputInfo.editorState;
			if ((inputInfo.inputEvent.button == 0 || inputInfo.inputEvent.button == 2) && state.focusedNode == null) 
			{
				state.panWindow = true;
				state.dragStart = inputInfo.inputPos;
				state.dragOffset = Vector2.zero;
			}
		}

		[EventHandlerAttribute (EventType.MouseDrag)]
		private static void HandleWindowPanning (NodeEditorInputInfo inputInfo) 
		{
			NodeEditorState state = inputInfo.editorState;
			if (state.panWindow) 
			{   Vector2 panOffsetChange = state.dragOffset;
				state.dragOffset = inputInfo.inputPos - state.dragStart;
				panOffsetChange = (state.dragOffset - panOffsetChange) * state.zoom;
				state.panOffset += panOffsetChange;
				NodeEditor.RepaintClients ();
			}
		}

		[EventHandlerAttribute (EventType.MouseDown)]
		[EventHandlerAttribute (EventType.MouseUp)]
		private static void HandleWindowPanningEnd (NodeEditorInputInfo inputInfo) 
		{
			inputInfo.editorState.panWindow = false;
		}

		#endregion

		#region Connection

		[EventHandlerAttribute (EventType.MouseDown)]
		private static void HandleConnectionDrawing (NodeEditorInputInfo inputInfo) 
		{
			NodeEditorState state = inputInfo.editorState;
			if (inputInfo.inputEvent.button == 0 && state.focusedNodeKnob != null)
			{ if (state.focusedNodeKnob is NodeOutput)
				{state.connectOutput = (NodeOutput)state.focusedNodeKnob;
					inputInfo.inputEvent.Use ();
				}
				else if (state.focusedNodeKnob is NodeInput)
				{ NodeInput clickedInput = (NodeInput)state.focusedNodeKnob;
					if (clickedInput.connection != null)
					{
						state.connectOutput = clickedInput.connection;
						clickedInput.RemoveConnection ();
						inputInfo.inputEvent.Use ();
					}
				}
			}
		}

		[EventHandlerAttribute (EventType.MouseUp)]
		private static void HandleApplyConnection (NodeEditorInputInfo inputInfo) 
		{
			NodeEditorState state = inputInfo.editorState;
			if (inputInfo.inputEvent.button == 0 && state.connectOutput != null && state.focusedNode != null && state.focusedNodeKnob != null && state.focusedNodeKnob is NodeInput) 
			{	NodeInput clickedInput = state.focusedNodeKnob as NodeInput;
				clickedInput.TryApplyConnection (state.connectOutput);
				inputInfo.inputEvent.Use ();
			}
			state.connectOutput = null;
		}

		#endregion

		#region Zoom

		[EventHandlerAttribute (EventType.ScrollWheel)]
		private static void HandleZooming (NodeEditorInputInfo inputInfo) 
		{
			inputInfo.editorState.zoom = (float)Math.Round (Math.Min (6.0, Math.Max (0.6, inputInfo.editorState.zoom + inputInfo.inputEvent.delta.y / 15)), 2);
			NodeEditor.RepaintClients ();
		}

		#endregion

		#region Navigation

		[HotkeyAttribute (KeyCode.N, EventType.KeyDown)]
		private static void HandleStartNavigating (NodeEditorInputInfo inputInfo) 
		{
			inputInfo.editorState.navigate = true;
		}

		[HotkeyAttribute (KeyCode.N, EventType.KeyUp)]
		private static void HandleEndNavigating (NodeEditorInputInfo inputInfo) 
		{
			inputInfo.editorState.navigate = false;
		}

		#endregion

		#region Node Snap

		[HotkeyAttribute (KeyCode.LeftControl, EventType.KeyDown, 60)] // 60 ensures it is checked after the dragging was performed before
		[HotkeyAttribute (KeyCode.LeftControl, EventType.KeyUp, 60)]
		private static void HandleNodeSnap (NodeEditorInputInfo inputInfo) 
		{
			NodeEditorState state = inputInfo.editorState;
			if (state.selectedNode != null)
			{ Vector2 pos = state.selectedNode.rect.position;
				pos = new Vector2 (Mathf.RoundToInt (pos.x/10) * 10, Mathf.RoundToInt (pos.y/10) * 10);
				state.selectedNode.rect.position = pos;
				inputInfo.inputEvent.Use ();
			}
			NodeEditor.RepaintClients ();
		}

		#endregion

	}

}

