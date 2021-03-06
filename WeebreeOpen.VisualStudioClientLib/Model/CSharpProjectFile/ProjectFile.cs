﻿namespace WeebreeOpen.VisualStudioClientLib.Model.CSharpProjectFile
{
    using System.Xml.Serialization;
    using WeebreeOpen.VisualStudioClientLib.Model.CSharpProjectFile;

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003", IsNullable = false)]
    public partial class Project
    {

        private ProjectImport[] importField;

        private ProjectPropertyGroup[] propertyGroupField;

        private ProjectItemGroup[] itemGroupField;

        private string toolsVersionField;

        private string defaultTargetsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Import")]
        public ProjectImport[] Import
        {
            get
            {
                return this.importField;
            }
            set
            {
                this.importField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PropertyGroup")]
        public ProjectPropertyGroup[] PropertyGroup
        {
            get
            {
                return this.propertyGroupField;
            }
            set
            {
                this.propertyGroupField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemGroup")]
        public ProjectItemGroup[] ItemGroup
        {
            get
            {
                return this.itemGroupField;
            }
            set
            {
                this.itemGroupField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ToolsVersion
        {
            get
            {
                return this.toolsVersionField;
            }
            set
            {
                this.toolsVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DefaultTargets
        {
            get
            {
                return this.defaultTargetsField;
            }
            set
            {
                this.defaultTargetsField = value;
            }
        }
    }

    /// <remarks/>

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectImport
    {

        private string projectField;

        private string conditionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Project
        {
            get
            {
                return this.projectField;
            }
            set
            {
                this.projectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }
    }

    /// <remarks/>

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectPropertyGroup
    {

        private string debugSymbolsField;

        private string debugTypeField;

        private string optimizeField;

        private string outputPathField;

        private string defineConstantsField;

        private string errorReportField;

        private string warningLevelField;

        private string projectGuidField;

        private string outputTypeField;

        private string appDesignerFolderField;

        private string rootNamespaceField;

        private string assemblyNameField;

        private string targetFrameworkVersionField;

        private string fileAlignmentField;

        private ProjectPropertyGroupConfiguration[] configurationField;

        private ProjectPropertyGroupPlatform[] platformField;

        private string conditionField;

        /// <remarks/>
        public string DebugSymbols
        {
            get
            {
                return this.debugSymbolsField;
            }
            set
            {
                this.debugSymbolsField = value;
            }
        }

        /// <remarks/>
        public string DebugType
        {
            get
            {
                return this.debugTypeField;
            }
            set
            {
                this.debugTypeField = value;
            }
        }

        /// <remarks/>
        public string Optimize
        {
            get
            {
                return this.optimizeField;
            }
            set
            {
                this.optimizeField = value;
            }
        }

        /// <remarks/>
        public string OutputPath
        {
            get
            {
                return this.outputPathField;
            }
            set
            {
                this.outputPathField = value;
            }
        }

        /// <remarks/>
        public string DefineConstants
        {
            get
            {
                return this.defineConstantsField;
            }
            set
            {
                this.defineConstantsField = value;
            }
        }

        /// <remarks/>
        public string ErrorReport
        {
            get
            {
                return this.errorReportField;
            }
            set
            {
                this.errorReportField = value;
            }
        }

        /// <remarks/>
        public string WarningLevel
        {
            get
            {
                return this.warningLevelField;
            }
            set
            {
                this.warningLevelField = value;
            }
        }

        /// <remarks/>
        public string ProjectGuid
        {
            get
            {
                return this.projectGuidField;
            }
            set
            {
                this.projectGuidField = value;
            }
        }

        /// <remarks/>
        public string OutputType
        {
            get
            {
                return this.outputTypeField;
            }
            set
            {
                this.outputTypeField = value;
            }
        }

        /// <remarks/>
        public string AppDesignerFolder
        {
            get
            {
                return this.appDesignerFolderField;
            }
            set
            {
                this.appDesignerFolderField = value;
            }
        }

        /// <remarks/>
        public string RootNamespace
        {
            get
            {
                return this.rootNamespaceField;
            }
            set
            {
                this.rootNamespaceField = value;
            }
        }

        /// <remarks/>
        public string AssemblyName
        {
            get
            {
                return this.assemblyNameField;
            }
            set
            {
                this.assemblyNameField = value;
            }
        }

        /// <remarks/>
        public string TargetFrameworkVersion
        {
            get
            {
                return this.targetFrameworkVersionField;
            }
            set
            {
                this.targetFrameworkVersionField = value;
            }
        }

        /// <remarks/>
        public string FileAlignment
        {
            get
            {
                return this.fileAlignmentField;
            }
            set
            {
                this.fileAlignmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Configuration", IsNullable = true)]
        public ProjectPropertyGroupConfiguration[] Configuration
        {
            get
            {
                return this.configurationField;
            }
            set
            {
                this.configurationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Platform", IsNullable = true)]
        public ProjectPropertyGroupPlatform[] Platform
        {
            get
            {
                return this.platformField;
            }
            set
            {
                this.platformField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }
    }

    /// <remarks/>

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectPropertyGroupConfiguration
    {

        private string conditionField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectPropertyGroupPlatform
    {

        private string conditionField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Condition
        {
            get
            {
                return this.conditionField;
            }
            set
            {
                this.conditionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectItemGroup
    {

        private ProjectItemGroupFolder[] folderField;

        private ProjectItemGroupCompile[] compileField;

        private ProjectItemGroupReference[] referenceField;

        private ProjectItemGroupProjectReference[] projectReferenceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Folder")]
        public ProjectItemGroupFolder[] Folder
        {
            get
            {
                return this.folderField;
            }
            set
            {
                this.folderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Compile")]
        public ProjectItemGroupCompile[] Compile
        {
            get
            {
                return this.compileField;
            }
            set
            {
                this.compileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Reference")]
        public ProjectItemGroupReference[] Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProjectReference")]
        public ProjectItemGroupProjectReference[] ProjectReference
        {
            get
            {
                return this.projectReferenceField;
            }
            set
            {
                this.projectReferenceField = value;
            }
        }
    }

    /// <remarks/>

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectItemGroupFolder
    {

        private string includeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }
    }



    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectItemGroupProjectReference
    {

        private string includeField;
        private string projectField;
        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }

        public string Project
        {
            get
            {
                return this.projectField;
            }
            set
            {
                this.projectField = value;
            }
        }

        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }


    /// <remarks/>

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectItemGroupCompile
    {

        private string includeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }
    }

    /// <remarks/>

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    public partial class ProjectItemGroupReference
    {

        private string includeField;
        private string hintPath;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Include
        {
            get
            {
                return this.includeField;
            }
            set
            {
                this.includeField = value;
            }
        }

        public string HintPath
        {
            get
            {
                return this.hintPath;
            }
            set
            {
                this.hintPath = value;
            }
        }
    }

    /// <remarks/>

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/developer/msbuild/2003")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/developer/msbuild/2003", IsNullable = false)]
    public partial class NewDataSet
    {

        private Project[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Project")]
        public Project[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
}