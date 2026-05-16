import { HttpErrorResponse } from '@angular/common/http';

type ErrorBody = {
  message?: string;
  Message?: string;
  title?: string;
  errors?: Record<string, string[]>;
};

export function getErrorMessage(error: unknown): string {
  if (!(error instanceof HttpErrorResponse)) {
    return 'Something went wrong. Please try again.';
  }

  const body: unknown = error.error;

  if (typeof body === 'string') {
    return body;
  }

  if (!body || typeof body !== 'object') {
    return error.message || 'Something went wrong. Please try again.';
  }

  const errorBody = body as ErrorBody;

  if (errorBody.errors) {
    const messages = Object.values(errorBody.errors).flat();

    if (messages.length > 0) {
      return messages.join(' ');
    }
  }

  return (
    errorBody.message ||
    errorBody.Message ||
    errorBody.title ||
    error.message ||
    'Something went wrong. Please try again.'
  );
}
