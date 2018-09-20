using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonerWinF
{

    [Serializable]
    public class PrisonerCollections : IList<Prisoner>
    {
        List<Prisoner> _collection;

        public PrisonerCollections()
        {
            _collection = new List<Prisoner>();

        }

        public Prisoner Peek()
        {
            return _collection[_collection.Count - 1];
        }

        public Prisoner this[int index]
        {
            get
            {
                return _collection[index];
            }

            set
            {
                _collection[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return _collection.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(Prisoner _prisoner)
        {
            
            _prisoner.ID = _collection.Count == 0
               ? 1
               : _collection[_collection.IndexOf(GetPrisonerWithMaxID())].ID + 1;

            _collection.Add(_prisoner);
        }

        public void AddCopy(Prisoner _prisoner)
        {
            _collection.Add(_prisoner);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(Prisoner _prisoner)
        {
            return _collection.Contains(_prisoner);
        }

        public void CopyTo(Prisoner[] array, int arrayIndex)
        {
            _collection.CopyTo(arrayIndex, array, 0, this.Count);
        }

        public IEnumerator<Prisoner> GetEnumerator()
        {
            foreach (var _prisoner in _collection)
            {
                yield return _prisoner;
            }
        }

        /// <summary>
        ///  Определяет индекс заданного элемента коллекции
        /// </summary>
        /// <param name="item">индекс искомого элемента</param>
        /// <returns></returns>
        public int IndexOf(Prisoner _prisoner)
        {
            return _collection.IndexOf(_prisoner);
        }

        public void Insert(int index, Prisoner _prisoner)
        {
            _collection.Insert(index, _prisoner);
        }

        public bool Remove(Prisoner _prisoner)
        {
            return _collection.Remove(_prisoner);
        }

        /// <summary>
        /// Удаляет элемент по указанному индексу.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _collection.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var _prisoner in _collection)
            {
                yield return _prisoner;
            }
        }

        public static explicit operator PrisonerCollections(List<Prisoner> list)
        {
            PrisonerCollections pC = new PrisonerCollections();
            for (int i = 0; i < list.Count; i++)
            {
                pC.AddCopy(list[i]);
            }
            return pC;
        }

        public string GetNameLastAddedPrisoner(out int id)
        {
            int _id = 0;
            id = 0;
            if (_collection.Count != 0)
            {
                id = _collection.Max(_p => _p.ID);
               _id = id;

            }
            return _collection.SingleOrDefault(p => p.ID == _id)?.FirstName;
        }

        public Prisoner GetPrisonerWithMaxID()
        {
            return _collection.SingleOrDefault<Prisoner>(x => x.ID == _collection.Max<Prisoner>(p => p.ID));
        }

    }
}
