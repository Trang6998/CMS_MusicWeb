

import { Pagination } from '@/models/Pagination';
import { T } from '@/models/T'; 

export interface PagedResult<T> { 
    pagination: Pagination;
    data: T[];
}