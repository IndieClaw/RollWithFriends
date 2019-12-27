// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>

#if UNITY_2019_3_OR_NEWER
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using System;
using System.Reflection;

namespace AmplifyShaderEditor
{
	public class ASEDiffusionProfile : MaterialPropertyDrawer
	{
		string m_hashField = string.Empty;

		public ASEDiffusionProfile( object guidField )
		{
			m_hashField = guidField.ToString();
		}

		public override void OnGUI( Rect position, MaterialProperty prop, String label, MaterialEditor editor )
		{
			var guid = HDUtils.ConvertVector4ToGUID( prop.vectorValue );
			var profile = AssetDatabase.LoadAssetAtPath<DiffusionProfileSettings>( AssetDatabase.GUIDToAssetPath( guid ) );

			EditorGUI.BeginChangeCheck();
			profile = EditorGUI.ObjectField( position, new GUIContent( label ), profile, typeof( DiffusionProfileSettings ), false ) as DiffusionProfileSettings;
			if( EditorGUI.EndChangeCheck() )
			{
				Vector4 newGuid = Vector4.zero;
				float hash = 0;
				if( profile != null )
				{
					var guid2 = AssetDatabase.AssetPathToGUID( AssetDatabase.GetAssetPath( profile ) );
					newGuid = HDUtils.ConvertGUIDToVector4( guid2 );
					hash = HDShadowUtilsEx.Asfloat( profile.profile.hash );
				}
				prop.vectorValue = newGuid;

				var hashField = MaterialEditor.GetMaterialProperty( new UnityEngine.Object[] { editor.target }, m_hashField );
				if( hashField != null )
				{
					hashField.floatValue = hash;
				}
			}

			if( profile == null )
				prop.vectorValue = Vector4.zero;
		}

		private static class HDShadowUtilsEx
		{
			private static System.Type type = null;
			public static System.Type Type { get { return ( type == null ) ? type = System.Type.GetType( "UnityEngine.Rendering.HighDefinition.HDShadowUtils, Unity.RenderPipelines.HighDefinition.Runtime" ) : type; } }

			public static float Asfloat( uint val )
			{
				object[] parameters = new object[] { val };
				MethodInfo method = Type.GetMethod( "Asfloat", new Type[] { typeof( uint ) } );
				return (float)method.Invoke( null, parameters );
			}

			public static uint Asuint( float val )
			{
				object[] parameters = new object[] { val };
				MethodInfo method = Type.GetMethod( "Asuint", new Type[] { typeof( float ) } );
				return (uint)method.Invoke( null, parameters );
			}
		}
	}
}
#endif //UNITY_2019_3_OR_NEWER
