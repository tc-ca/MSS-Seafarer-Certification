// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "As a team we decided not to support header comments")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements should be ordered by access", Justification = "We have decided that we'd rather have functions closer to their use than ordered by public / private", Scope = "member", Target = "~M:CDNApplication.Utilities.SessionHelper.GetLanguageFromPath(System.String)~System.String")]
