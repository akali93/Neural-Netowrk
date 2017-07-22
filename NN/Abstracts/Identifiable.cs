using System;

namespace NN
{
    public class Identifiable : IIdentifiable
    {
        /// <summary>
        /// This counts the Identifiable objects for this session.
        /// </summary>
        private static int idCouner = 1;

        /// <summary>
        /// A unique ID for internal network use.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Create a new Identifiable instance.
        /// </summary>
        public Identifiable()
        {
            ID = idCouner++;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            if (other is IIdentifiable)
                return Equals((IIdentifiable)other);
            return false;
        }

        /// <summary>
        /// Determines whether the specified Identifiable is equal to the current Identifiable.
        /// </summary>
        /// <param name="other">The Identifiable to compare with the current Identifiable.</param>
        /// <returns></returns>
        public bool Equals(IIdentifiable other)
        {
            return ID == other.ID;
        }

        /// <summary>
        /// Returns the hash code for the current object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ID: " + ID;
        }
    }
}
