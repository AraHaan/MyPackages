// Copyright (c) 2019-2020, AraHaan.
// https://github.com/AraHaan/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace System.Runtime.InteropServices
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Obtain the git repository information for the assembly.
    /// </summary>
    public class GitInformation
    {
        // This is the collection of instances this has.
        private static readonly Dictionary<Assembly, GitInformation> AssemblyInstances = new Dictionary<Assembly, GitInformation>();
        private static readonly HashSet<Assembly> AppliedAssemblies = new HashSet<Assembly>();

        internal GitInformation(string headdesc, string commit, string branchname, Assembly assembly)
        {
            this.Headdesc = headdesc;
            this.Commit = commit;
            this.Branchname = branchname;

            if (!AssemblyInstances.ContainsKey(assembly))
            {
                AssemblyInstances.Add(assembly, this);
            }
        }

        /// <summary>
        /// Gets the git commit head description based upon the string constructed by
        /// git name-rev.
        /// </summary>
        public string Headdesc { get; private set; }

        /// <summary>
        /// Gets the git commit hash as formated by git rev-parse.
        /// </summary>
        /// <value>
        /// The git commit hash as formated by git rev-parse.
        /// </value>
        public string Commit { get; private set; }

        /// <summary>
        /// Gets the git branch name as formated by git name-rev.
        /// </summary>
        /// <value>
        /// The git branch name as formated by git name-rev.
        /// </value>
        public string Branchname { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the branch is dirty or
        /// clean based upon the string constructed by git describe.
        /// </summary>
        /// <value>
        /// A value indicating whether the branch is dirty or
        /// clean based upon the string constructed by git describe.
        /// </value>
        public bool IsDirty => this.Headdesc.EndsWith("-dirty", StringComparison.Ordinal);

        /// <summary>
        /// Gets a value indicating whether the branch is the master
        /// branch or not based upon the string constructed by
        /// git name-rev.
        /// </summary>
        /// <value>
        /// A value indicating whether the branch is the master
        /// branch or not based upon the string constructed by
        /// git name-rev.
        /// </value>
        public bool IsMaster => this.Branchname.Equals("master", StringComparison.Ordinal);

        /// <summary>
        /// Applies the <see cref="Attribute"/>s that the specified <see cref="Assembly"/> contains.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="Assembly"/> to apply the attributes to.
        /// </param>
        public static void ApplyAssemblyAttributes(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            // this check is to avoid a stack overflow exception.
            var added = AppliedAssemblies.Add(assembly);
            if (added)
            {
                _ = assembly.GetCustomAttributes(false);
            }
        }

        /// <summary>
        /// Gets the instance of the <see cref="GitInformation"/> class for
        /// the specified <see cref="Type"/> or <see langword="null"/>.
        /// </summary>
        /// <param name="assemblyType">
        /// The <see cref="Type"/> to the <see cref="Assembly"/> to look for a instance of the <see cref="GitInformation"/> class.
        /// </param>
        /// <returns>
        /// The <see cref="GitInformation"/> class instance to the specified <see cref="Type"/>
        /// or <see langword="null"/>.
        /// </returns>
        public static GitInformation GetAssemblyInstance(Type assemblyType)
        {
            if (assemblyType == null)
            {
                throw new ArgumentNullException(nameof(assemblyType));
            }

            return GetAssemblyInstance(assemblyType.Assembly);
        }

        /// <summary>
        /// Gets the instance of the <see cref="GitInformation"/> class for
        /// the specified <see cref="Assembly"/> or <see langword="null"/>.
        /// </summary>
        /// <param name="assembly">
        /// The <see cref="Assembly"/> to look for a instance of the <see cref="GitInformation"/> class.
        /// </param>
        /// <returns>
        /// The <see cref="GitInformation"/> class instance to the specified <see cref="Assembly"/>
        /// or <see langword="null"/>.
        /// </returns>
        public static GitInformation GetAssemblyInstance(Assembly assembly)
            => AssemblyInstances.TryGetValue(assembly, out var gitInformation) ? gitInformation : null;
    }
}
