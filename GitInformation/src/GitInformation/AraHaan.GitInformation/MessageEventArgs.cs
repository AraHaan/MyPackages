﻿// Copyright (c) 2019-2020, AraHaan.
// https://github.com/AraHaan/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace System.Messaging
{
    /// <summary>
    /// Event that holds the message text and the caption.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="text">The text for the message.</param>
        /// <param name="caption">The title (caption) for the message.</param>
        /// <param name="errorlevel">The error level for the message, or None for no error level information.</param>
        public MessageEventArgs(string text, string caption, ErrorLevel errorlevel)
        {
            this.Text = text;
            this.Caption = caption;
            this.ErrorLevel = errorlevel;
        }

        /// <summary>
        /// Gets or sets the text for the message.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the caption (title) for the message.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ErrorLevel"/> of the message.
        /// </summary>
        public ErrorLevel ErrorLevel { get; set; }
    }
}
