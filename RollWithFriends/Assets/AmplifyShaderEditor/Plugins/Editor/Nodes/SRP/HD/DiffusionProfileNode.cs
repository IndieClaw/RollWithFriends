// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine;
#if UNITY_2019_3_OR_NEWER
using UnityEngine.Rendering.HighDefinition;
#else
using UnityEngine.Experimental.Rendering.HDPipeline;
#endif
using System;
using System.Text;
using System.Reflection;

namespace AmplifyShaderEditor
{
	[Serializable]
	[NodeAttributes( "Diffusion Profile", "Constants And Properties", "Returns Diffusion Profile Hash Id. To be used on Diffusion Profile port on HDRP templates.", KeyCode.None, true, 0, int.MaxValue, typeof( DiffusionProfileSettings ) )]
	public sealed class DiffusionProfileNode : PropertyNode
	{
		[SerializeField]
		private DiffusionProfileSettings m_defaultValue;

		[SerializeField]
		private DiffusionProfileSettings m_materialValue;

		private bool m_isEditingFields;
		//[NonSerialized]
		//private DiffusionProfileSettings m_previousValue;

		protected override void CommonInit( int uniqueId )
		{
			base.CommonInit( uniqueId );
			AddOutputPort( WirePortDataType.FLOAT, Constants.EmptyPortValue );
			m_drawPrecisionUI = false;
			m_currentPrecisionType = PrecisionType.Float;
			m_srpBatcherCompatible = true;
			m_freeType = false;
#if UNITY_2019_3_OR_NEWER
			m_freeType = true;
#endif
		}

		protected override void OnUniqueIDAssigned()
		{
			base.OnUniqueIDAssigned();
			UIUtils.RegisterPropertyNode( this );
		}

		public override void CopyDefaultsToMaterial()
		{
			m_materialValue = m_defaultValue;
		}

		public override void DrawSubProperties()
		{
			m_defaultValue = EditorGUILayoutObjectField( Constants.DefaultValueLabel, m_defaultValue, typeof( DiffusionProfileSettings ), true ) as DiffusionProfileSettings;
		}

		public override void DrawMaterialProperties()
		{
			if( m_materialMode )
				EditorGUI.BeginChangeCheck();

			m_materialValue = EditorGUILayoutObjectField( Constants.MaterialValueLabel, m_materialValue, typeof( DiffusionProfileSettings ), true ) as DiffusionProfileSettings;

			if( m_materialMode && EditorGUI.EndChangeCheck() )
			{
				m_requireMaterialUpdate = true;
			}
		}

		//public override void OnNodeLayout( DrawInfo drawInfo )
		//{
		//	base.OnNodeLayout( drawInfo );

		//	m_propertyDrawPos = m_remainingBox;
		//	m_propertyDrawPos.width = drawInfo.InvertedZoom * Constants.FLOAT_DRAW_WIDTH_FIELD_SIZE * 2;
		//	m_propertyDrawPos.height = drawInfo.InvertedZoom * Constants.FLOAT_DRAW_HEIGHT_FIELD_SIZE;
		//}

		//public override void DrawGUIControls( DrawInfo drawInfo )
		//{
		//	base.DrawGUIControls( drawInfo );

		//	if( drawInfo.CurrentEventType != EventType.MouseDown )
		//		return;

		//	Rect hitBox = m_remainingBox;
		//	bool insideBox = hitBox.Contains( drawInfo.MousePosition );

		//	if( insideBox )
		//	{
		//		GUI.FocusControl( null );
		//		m_isEditingFields = true;
		//	}
		//	else if( m_isEditingFields && !insideBox )
		//	{
		//		GUI.FocusControl( null );
		//		m_isEditingFields = false;
		//	}
		//}

		//GUIStyle GetStyle( string styleName )
		//{
		//	GUIStyle s = GUI.skin.FindStyle( styleName ) ?? EditorGUIUtility.GetBuiltinSkin( EditorSkin.Inspector ).FindStyle( styleName );
		//	if( s == null )
		//	{
		//		Debug.LogError( "Missing built-in guistyle " + styleName );
		//		s = GUIStyle.none;
		//	}
		//	return s;
		//}

		//public override void Draw( DrawInfo drawInfo )
		//{
		//	base.Draw( drawInfo );

		//	if( !m_isVisible )
		//		return;

		//	var cache = EditorStyles.objectField.fontSize;
		//	EditorStyles.objectField.fontSize = (int)(9 * drawInfo.InvertedZoom);
		//	var style = GetStyle( "ObjectFieldButton" );
		//	var sw = style.stretchWidth;
		//	style.stretchWidth = false;
		//	//style.isHeightDependantOnWidth
		//	style.fixedWidth = (int)( 16 * drawInfo.InvertedZoom );
		//	style.fixedHeight = (int)( 16 * drawInfo.InvertedZoom );
		//	//if( m_isEditingFields && m_currentParameterType != PropertyType.Global )
		//	//{
		//	float labelWidth = EditorGUIUtility.labelWidth;
		//		EditorGUIUtility.labelWidth = 0;

		//		if( m_materialMode && m_currentParameterType != PropertyType.Constant )
		//		{
		//			EditorGUI.BeginChangeCheck();
		//			m_materialValue = EditorGUIObjectField( m_propertyDrawPos, m_materialValue, typeof( DiffusionProfileSettings ), true ) as DiffusionProfileSettings;
		//			if( EditorGUI.EndChangeCheck() )
		//			{
		//				PreviewIsDirty = true;
		//				m_requireMaterialUpdate = true;
		//				if( m_currentParameterType != PropertyType.Constant )
		//					BeginDelayedDirtyProperty();
		//			}
		//		}
		//		else
		//		{
		//			EditorGUI.BeginChangeCheck();
		//			m_defaultValue = EditorGUIObjectField( m_propertyDrawPos, m_defaultValue, typeof( DiffusionProfileSettings ), true ) as DiffusionProfileSettings;
		//			if( EditorGUI.EndChangeCheck() )
		//			{
		//				PreviewIsDirty = true;
		//				BeginDelayedDirtyProperty();
		//			}
		//		}
		//		EditorGUIUtility.labelWidth = labelWidth;

		//	style.fixedWidth = 0;
		//	style.fixedHeight = 0;
		//	style.stretchWidth = sw;
		//	EditorStyles.objectField.fontSize = cache;
		//	//}
		//	//else if( drawInfo.CurrentEventType == EventType.Repaint )
		//	//{
		//	//	bool guiEnabled = GUI.enabled;
		//	//	GUI.enabled = m_currentParameterType != PropertyType.Global;
		//	//	Rect fakeField = m_propertyDrawPos;
		//	//	if( GUI.enabled )
		//	//	{
		//	//		Rect fakeLabel = m_propertyDrawPos;
		//	//		fakeLabel.xMax = fakeField.xMin;
		//	//		EditorGUIUtility.AddCursorRect( fakeLabel, MouseCursor.SlideArrow );
		//	//		EditorGUIUtility.AddCursorRect( fakeField, MouseCursor.Text );
		//	//	}
		//	//	bool currMode = m_materialMode && m_currentParameterType != PropertyType.Constant;
		//	//	var value = currMode ? m_materialValue : m_defaultValue;

		//	//	//if( m_previousValue != value )
		//	//	//{
		//	//	//	m_previousValue = value;
		//	//	// string stuff
		//	//	//}

		//	//	//GUI.Label( fakeField, m_fieldText, UIUtils.MainSkin.textField );
		//	//	GUI.enabled = guiEnabled;
		//	//}
		//}


		public override string GenerateShaderForOutput( int outputId, ref MasterNodeDataCollector dataCollector, bool ignoreLocalvar )
		{
			base.GenerateShaderForOutput( outputId, ref dataCollector, ignoreLocalvar );

			if( m_currentParameterType != PropertyType.Constant )
				return PropertyData( dataCollector.PortCategory );

#if UNITY_2019_3_OR_NEWER
			return RoundTrip.ToRoundTrip( HDShadowUtilsEx.Asfloat( DefaultHash ) );
#else
			return "asfloat(" + DefaultHash.ToString() + ")";
#endif
		}

		public override string GetPropertyValue()
		{
			Vector4 asset = Vector4.zero;
			if( m_defaultValue != null )
				asset = HDUtils.ConvertGUIDToVector4( AssetDatabase.AssetPathToGUID( AssetDatabase.GetAssetPath( m_defaultValue ) ) );
			string assetVec = RoundTrip.ToRoundTrip( asset.x ) + ", " + RoundTrip.ToRoundTrip( asset.y ) + ", " + RoundTrip.ToRoundTrip( asset.z ) + ", " + RoundTrip.ToRoundTrip( asset.w );
			string lineOne = PropertyAttributes + "[ASEDiffusionProfile(" + m_propertyName + ")]" + m_propertyName + "_asset(\"" + m_propertyInspectorName + "\", Vector) = ( " + assetVec + " )";
			string lineTwo = "\n[HideInInspector]" + m_propertyName + "(\"" + m_propertyInspectorName + "\", Float) = " + RoundTrip.ToRoundTrip( HDShadowUtilsEx.Asfloat( DefaultHash ) );

			return lineOne + lineTwo;
		}

		public override void UpdateMaterial( Material mat )
		{
			base.UpdateMaterial( mat );

			if( UIUtils.IsProperty( m_currentParameterType ) && !InsideShaderFunction )
			{
				if( m_materialValue != null )
				{
					Vector4 asset = HDUtils.ConvertGUIDToVector4( AssetDatabase.AssetPathToGUID( AssetDatabase.GetAssetPath( m_materialValue ) ) );
					mat.SetVector( m_propertyName + "_asset", asset );
					mat.SetFloat( m_propertyName, HDShadowUtilsEx.Asfloat( MaterialHash ) );
				}
			}
		}

		public override void ForceUpdateFromMaterial( Material material )
		{
			if( UIUtils.IsProperty( m_currentParameterType ) && material.HasProperty( m_propertyName+"_asset" ) )
			{
				var guid = HDUtils.ConvertVector4ToGUID( material.GetVector( m_propertyName + "_asset" ) );
				var profile = AssetDatabase.LoadAssetAtPath<DiffusionProfileSettings>( AssetDatabase.GUIDToAssetPath( guid ) );
				if( profile != null )
					m_materialValue = profile;
			}
		}

		public override void WriteToString( ref string nodeInfo, ref string connectionsInfo )
		{
			base.WriteToString( ref nodeInfo, ref connectionsInfo );
			string defaultGuid = ( m_defaultValue != null ) ? AssetDatabase.AssetPathToGUID( AssetDatabase.GetAssetPath( m_defaultValue ) ) : "0";
			IOUtils.AddFieldValueToString( ref nodeInfo, defaultGuid );
			string materialGuid = ( m_materialValue != null ) ? AssetDatabase.AssetPathToGUID( AssetDatabase.GetAssetPath( m_materialValue ) ) : "0";
			IOUtils.AddFieldValueToString( ref nodeInfo, materialGuid );
		}

		public override void ReadFromString( ref string[] nodeParams )
		{
			if( UIUtils.CurrentShaderVersion() > 17004 )
				base.ReadFromString( ref nodeParams );
			else
				ParentReadFromString( ref nodeParams );

			string defaultGuid = GetCurrentParam( ref nodeParams );
			if( defaultGuid.Length > 1 )
			{
				m_defaultValue = AssetDatabase.LoadAssetAtPath<DiffusionProfileSettings>( AssetDatabase.GUIDToAssetPath( defaultGuid ) );
			}
			if( UIUtils.CurrentShaderVersion() > 17004 )
			{
				string materialGuid = GetCurrentParam( ref nodeParams );
				if( materialGuid.Length > 1 )
				{
					m_materialValue = AssetDatabase.LoadAssetAtPath<DiffusionProfileSettings>( AssetDatabase.GUIDToAssetPath( materialGuid ) );
				}
			}
		}

		public override void ReadOutputDataFromString( ref string[] nodeParams )
		{
			base.ReadOutputDataFromString( ref nodeParams );
			if( UIUtils.CurrentShaderVersion() < 17005 )
				m_outputPorts[ 0 ].ChangeProperties( Constants.EmptyPortValue, WirePortDataType.FLOAT, false );
		}

		public override string GetPropertyValStr()
		{
			return PropertyName;
		}

		//Vector4 ProfileGUID { get { return ( m_diffusionProfile != null ) ? HDUtils.ConvertGUIDToVector4( AssetDatabase.AssetPathToGUID( AssetDatabase.GetAssetPath( m_diffusionProfile ) ) ) : Vector4.zero; } }
		uint DefaultHash { get { return ( m_defaultValue != null ) ? m_defaultValue.profile.hash : 0; } }
		uint MaterialHash { get { return ( m_materialValue != null ) ? m_materialValue.profile.hash : 0; } }

		private static class HDShadowUtilsEx
		{
			private static System.Type type = null;
			public static System.Type Type { get { return ( type == null ) ? type = System.Type.GetType( "UnityEngine.Rendering.HighDefinition.HDShadowUtils, Unity.RenderPipelines.HighDefinition.Runtime" ) : type; } }

			public static float Asfloat( uint val )
			{
#if UNITY_2019_3_OR_NEWER
				object[] parameters = new object[] { val };
				MethodInfo method = Type.GetMethod( "Asfloat", new Type[] { typeof( uint ) } );
				return (float)method.Invoke( null, parameters );
#else
				return HDShadowUtils.Asfloat( val );
#endif
			}

			public static uint Asuint( float val )
			{
#if UNITY_2019_3_OR_NEWER

				object[] parameters = new object[] { val };
				MethodInfo method = Type.GetMethod( "Asuint", new Type[] { typeof( float ) } );
				return (uint)method.Invoke( null, parameters );
#else
				return HDShadowUtils.Asuint( val );
#endif
			}
		}

		private static class RoundTrip
		{
			private static String[] zeros = new String[ 1000 ];

			static RoundTrip()
			{
				for( int i = 0; i < zeros.Length; i++ )
				{
					zeros[ i ] = new String( '0', i );
				}
			}

			public static String ToRoundTrip( double value )
			{
				String str = value.ToString( "r" );
				int x = str.IndexOf( 'E' );
				if( x < 0 ) return str;

				int x1 = x + 1;
				String exp = str.Substring( x1, str.Length - x1 );
				int e = int.Parse( exp );

				String s = null;
				int numDecimals = 0;
				if( value < 0 )
				{
					int len = x - 3;
					if( e >= 0 )
					{
						if( len > 0 )
						{
							s = str.Substring( 0, 2 ) + str.Substring( 3, len );
							numDecimals = len;
						}
						else
							s = str.Substring( 0, 2 );
					}
					else
					{
						// remove the leading minus sign
						if( len > 0 )
						{
							s = str.Substring( 1, 1 ) + str.Substring( 3, len );
							numDecimals = len;
						}
						else
							s = str.Substring( 1, 1 );
					}
				}
				else
				{
					int len = x - 2;
					if( len > 0 )
					{
						s = str[ 0 ] + str.Substring( 2, len );
						numDecimals = len;
					}
					else
						s = str[ 0 ].ToString();
				}

				if( e >= 0 )
				{
					e = e - numDecimals;
					String z = ( e < zeros.Length ? zeros[ e ] : new String( '0', e ) );
					s = s + z;
				}
				else
				{
					e = ( -e - 1 );
					String z = ( e < zeros.Length ? zeros[ e ] : new String( '0', e ) );
					if( value < 0 )
						s = "-0." + z + s;
					else
						s = "0." + z + s;
				}

				return s;
			}
		}
	}
}
#endif
