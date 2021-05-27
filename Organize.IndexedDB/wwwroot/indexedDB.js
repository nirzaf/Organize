organizeIndexedDB = {
    db: {},

    initAsync: async function () {
        const openRequestPromise = new Promise((resolve, reject) => {
            const request = window.indexedDB.open("organize");
            request.onupgradeneeded = this.onUpgradeNeeded;

            request.onsuccess = function (event) {
                //setTimeout(() => resolve(event.target.result), 4000);
                resolve(event.target.result);
            }
            request.onerror = function (event) {
                reject(event);
            }
        });
        //await this.timeout(5000);
        this.db = await openRequestPromise;
    },

    onUpgradeNeeded: function (event) {
        const db = event.target.result;
        db.createObjectStore("User", { keyPath: "id", autoIncrement: true });

        const textItemStore = db.createObjectStore("TextItem", { keyPath: "id", autoIncrement: true });
        textItemStore.createIndex("parentId", "parentId", { unique: false });

        const urlItemStore = db.createObjectStore("UrlItem", { keyPath: "id", autoIncrement: true });
        urlItemStore.createIndex("parentId", "parentId", { unique: false });

        const parentItemStore = db.createObjectStore("ParentItem", { keyPath: "id", autoIncrement: true });
        parentItemStore.createIndex("parentId", "parentId", { unique: false });

        const childItemStore = db.createObjectStore("ChildItem", { keyPath: "id", autoIncrement: true });
        childItemStore.createIndex("parentId", "parentId", { unique: false });
    },

    getAllAsync: async function (tableName) {
        return await new Promise((resolve, reject) => {
            const transaction = this.db.transaction(tableName);
            transaction.onerror = function (event) {
                reject(event);
            };
            const store = transaction.objectStore(tableName);
            const elements = [];
            store.openCursor().onsuccess = function (event) {
                var cursor = event.target.result;
                if (cursor) {
                    elements.push(cursor.value);
                    cursor.continue();
                } else {
                    resolve(elements);
                }
            };
        });
    },

    addAsync: async function (tableName, entityToAdd) {
        console.log(entityToAdd);
        entityToAdd = JSON.parse(entityToAdd);
        console.log(entityToAdd);
        delete entityToAdd.id;

        return await new Promise((resolve, reject) => {
            const transaction = this.db.transaction(tableName, "readwrite");
            transaction.onerror = function (event) {
                reject(event);
            };
            const store = transaction.objectStore(tableName);

            const request = store.add(entityToAdd);
            request.onsuccess = function (event) {
                //Returns the id of the entity
                resolve(event.target.result);
            };
        });
    },

    putAsync: async function (tableName, entityToPut, id) {
        entityToPut = JSON.parse(entityToPut);
        return await new Promise((resolve, reject) => {
            const transaction = this.db.transaction(tableName, "readwrite");
            transaction.onerror = function (event) {
                reject(event);
            };

            const store = transaction.objectStore(tableName);
            entityToPut.id = id;
            const request = store.put(entityToPut);

            request.onsuccess = function (event) {
                resolve();
            };
        });
    },
    deleteAsync: async function (tableName, id) {
        return await new Promise((resolve, reject) => {
            const transaction = this.db.transaction(tableName, "readwrite");
            transaction.onerror = function (event) {
                reject(event);
            };

            const store = transaction.objectStore(tableName);
            const request = store.delete(id);
            request.onsuccess = function (event) {
                resolve();
            };
        });
    },
}