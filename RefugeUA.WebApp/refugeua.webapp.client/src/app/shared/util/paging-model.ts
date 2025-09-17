export interface PagingInfo<TModel> {
    items: TModel[],
    totalCount: number,
    page: number,
    pageLength: number,
    pagesCount: number
}