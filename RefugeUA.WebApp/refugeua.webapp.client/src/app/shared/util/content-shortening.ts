export const shortenContent = (content: string, maxLength: number): string => {
    if (content.length! > maxLength) {
        return content.substring(0, maxLength) + '...';
    }

    return content!;
}