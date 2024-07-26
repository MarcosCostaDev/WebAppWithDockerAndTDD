export type ApiResponse<T> = {
    data:T;
    errors: string[];
    valid: boolean
}