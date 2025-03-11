import { TypeResponse } from './type-response';

export interface ApiResponse {
    type: TypeResponse;
    message: string;
    data: any;
}

export interface ApiResponseData<T> {
    type: TypeResponse;
    message: string;
    data: T;
}