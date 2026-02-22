export function initDb(dbName, version, stores) {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(dbName, version);

        request.onupgradeneeded = (event) => {
            const db = event.target.result;
            stores.forEach(storeName => {
                if (!db.objectStoreNames.contains(storeName)) {
                    db.createObjectStore(storeName, { keyPath: 'id', autoIncrement: true });
                }
            });
        };

        request.onsuccess = () => resolve(true);
        request.onerror = () => reject(request.error);
    });
}

export function addObject(dbName, storeName, object) {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(dbName);
        request.onsuccess = (event) => {
            const db = event.target.result;
            const transaction = db.transaction([storeName], 'readwrite');
            const store = transaction.objectStore(storeName);
            const addRequest = store.add(object);
            addRequest.onsuccess = () => resolve(true);
            addRequest.onerror = () => reject(addRequest.error);
        };
        request.onerror = () => reject(request.error);
    });
}

export function addObjects(dbName, storeName, objects) {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(dbName);
        request.onsuccess = (event) => {
            const db = event.target.result;
            const transaction = db.transaction([storeName], 'readwrite');
            const store = transaction.objectStore(storeName);
            
            let count = 0;
            objects.forEach(obj => {
                const addReq = store.add(obj);
                addReq.onsuccess = () => {
                    count++;
                    if (count === objects.length) resolve(true);
                };
            });

            transaction.oncomplete = () => resolve(true);
            transaction.onerror = () => reject(transaction.error);
        };
        request.onerror = () => reject(request.error);
    });
}

export function getObjects(dbName, storeName) {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(dbName);
        request.onsuccess = (event) => {
            const db = event.target.result;
            const transaction = db.transaction([storeName], 'readonly');
            const store = transaction.objectStore(storeName);
            const getRequest = store.getAll();
            getRequest.onsuccess = () => resolve(getRequest.result);
            getRequest.onerror = () => reject(getRequest.error);
        };
        request.onerror = () => reject(request.error);
    });
}

export function clearStore(dbName, storeName) {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(dbName);
        request.onsuccess = (event) => {
            const db = event.target.result;
            const transaction = db.transaction([storeName], 'readwrite');
            const store = transaction.objectStore(storeName);
            const clearRequest = store.clear();
            clearRequest.onsuccess = () => resolve(true);
            clearRequest.onerror = () => reject(clearRequest.error);
        };
        request.onerror = () => reject(request.error);
    });
}
