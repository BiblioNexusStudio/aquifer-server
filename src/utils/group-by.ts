interface Array<T> {
    groupBy(keyFunction: (item: T) => any): Record<any, T[]>;
}

Array.prototype.groupBy = function <T>(keyFunction: (item: T) => any): Record<any, T[]> {
    const groups = {};
    this.forEach(function (el: T) {
        const key = keyFunction(el);
        if (key in groups == false) {
            groups[key] = [];
        }
        groups[key].push(el);
    });
    return groups;
};
