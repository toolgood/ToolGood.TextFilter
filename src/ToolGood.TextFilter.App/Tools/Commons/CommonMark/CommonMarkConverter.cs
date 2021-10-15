using CommonMark.Parser;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace CommonMark
{
    /// <summary>
    /// Contains methods for parsing and formatting CommonMark data.
    /// </summary>
    public static class CommonMarkConverter
    {

        /// <summary>
        /// Performs the first stage of the conversion - parses block elements from the source and created the syntax tree.
        /// </summary>
        /// <param name="source">The reader that contains the source data.</param>
        /// <param name="settings">The object containing settings for the parsing process.</param>
        /// <returns>The block element that represents the document.</returns>
        /// <exception cref="ArgumentNullException">when <paramref name="source"/> is <see langword="null"/></exception>
        /// <exception cref="CommonMarkException">when errors occur during block parsing.</exception>
        /// <exception cref="IOException">when error occur while reading the data.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Syntax.Block ProcessStage1(TextReader source, CommonMarkSettings settings = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (settings == null)
                settings = CommonMarkSettings.Default;

            var cur = Syntax.Block.CreateDocument();
            var doc = cur;
            var line = new LineInfo(settings.TrackSourcePosition);

            try
            {
                var reader = new TabTextReader(source);
                reader.ReadLine(line);
                while (line.Line != null)
                {
                    BlockMethods.IncorporateLine(line, ref cur);
                    reader.ReadLine(line);
                }
            }
            catch(IOException)
            {
                throw;
            }
            catch(CommonMarkException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new CommonMarkException("An error occurred while parsing line " + line.ToString(), cur, ex);
            }

            try
            {
                do
                {
                    BlockMethods.Finalize(cur, line);
                    cur = cur.Parent;
                } while (cur != null);
            }
            catch (CommonMarkException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new CommonMarkException("An error occurred while finalizing open containers.", cur, ex);
            }

            return doc;
        }

        /// <summary>
        /// Performs the second stage of the conversion - parses block element contents into inline elements.
        /// </summary>
        /// <param name="document">The top level document element.</param>
        /// <param name="settings">The object containing settings for the parsing process.</param>
        /// <exception cref="ArgumentException">when <paramref name="document"/> does not represent a top level document.</exception>
        /// <exception cref="ArgumentNullException">when <paramref name="document"/> is <see langword="null"/></exception>
        /// <exception cref="CommonMarkException">when errors occur during inline parsing.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ProcessStage2(Syntax.Block document, CommonMarkSettings settings = null)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            if (document.Tag != Syntax.BlockTag.Document)
                throw new ArgumentException("The block element passed to this method must represent a top level document.", nameof(document));

            if (settings == null)
                settings = CommonMarkSettings.Default;

            try
            {
                BlockMethods.ProcessInlines(document, document.Document, settings);
            }
            catch(CommonMarkException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new CommonMarkException("An error occurred during inline parsing.", ex);
            }
        }


        /// <summary>
        /// Parses the given source data and returns the document syntax tree. Use <see cref="ProcessStage3"/> to
        /// convert the document to HTML using the built-in converter.
        /// </summary>
        /// <param name="source">The reader that contains the source data.</param>
        /// <param name="settings">The object containing settings for the parsing and formatting process.</param>
        /// <exception cref="ArgumentNullException">when <paramref name="source"/> is <see langword="null"/></exception>
        /// <exception cref="CommonMarkException">when errors occur during parsing.</exception>
        /// <exception cref="IOException">when error occur while reading or writing the data.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Syntax.Block Parse(TextReader source, CommonMarkSettings settings = null)
        {
            if (settings == null)
                settings = CommonMarkSettings.Default;

            var document = ProcessStage1(source, settings);
            ProcessStage2(document, settings);
            return document;
        }

        /// <summary>
        /// Parses the given source data and returns the document syntax tree. Use <see cref="ProcessStage3"/> to
        /// convert the document to HTML using the built-in converter.
        /// </summary>
        /// <param name="source">The source data.</param>
        /// <param name="settings">The object containing settings for the parsing and formatting process.</param>
        /// <exception cref="ArgumentNullException">when <paramref name="source"/> is <see langword="null"/></exception>
        /// <exception cref="CommonMarkException">when errors occur during parsing.</exception>
        /// <exception cref="IOException">when error occur while reading or writing the data.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Syntax.Block Parse(string source, CommonMarkSettings settings = null)
        {
            if (source == null)
                return null;

            using (var reader = new System.IO.StringReader(source))
                return Parse(reader, settings);
        }

 
    }
}
