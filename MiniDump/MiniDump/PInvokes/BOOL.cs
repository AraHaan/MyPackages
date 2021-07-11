// Copyright (c) 2018-2021, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Elskom.Generic.Libs
{
    using global::System;

    internal readonly struct BOOL : IEquatable<BOOL>
    {
        private BOOL(bool value)
            => this.Value = value ? 1 : 0;

        private BOOL(int value)
            => this.Value = value;

        private int Value { get; }

        public static implicit operator bool(BOOL value)
            => value.Value != 0;

        public static implicit operator BOOL(bool value)
            => new(value);

        public static explicit operator BOOL(int value)
            => new(value);

        public bool Equals(BOOL other)
            => this.Value == other.Value;

        public override bool Equals(object obj)
            => obj is BOOL b && this.Equals(b);

        public override int GetHashCode()
            => this.Value;
    }
}
