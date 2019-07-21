using UnityEngine;

namespace Modules.VirtualBook {
    public class TestBasicBook : MonoBehaviour {
        private void Start() {
            GameObject virtualLibraryObject = new GameObject("VirtualLib");
            virtualLibraryObject.AddComponent<VirtualLibrary>();
            VirtualLibrary virtualLibrary = virtualLibraryObject.GetComponent<VirtualLibrary>();
            string bookId = virtualLibrary.randomBookId();
            VirtualBook.createFromId(bookId);
        }
    }
}