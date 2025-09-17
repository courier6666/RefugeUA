export const IsValueNullNanOrUndefined = (obj: any): boolean => {
    return obj == null || Number.isNaN(obj) || obj == undefined;
}