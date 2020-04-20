

 

export interface Pagination { 
    page: number;
    itemsPerPage: number;
    records: number;
    totalItems: number;
    totalPages: number;
    sortBy: string;
    descending: boolean;
    includeEntities: boolean;
}