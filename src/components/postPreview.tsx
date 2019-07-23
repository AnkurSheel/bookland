import { Link } from 'gatsby';
import React from 'react';
import { css } from '@emotion/core';
import ReadLink from './readLink';
// import ReadLink from './read-link';

const PostPreview = ({ post }: any) => {
    return (
        <article
            css={css`
                border-bottom: 1px solid #ddd;
                padding-bottom: 1rem;
                margin-top: 0.75rem;

                &:first-of-type {
                    margin-top: 1rem;
                }
            `}
        >
            <h3>
                <Link to={post.slug}>{post.title}</Link>
            </h3>
            <h3>{post.excerpt}</h3>
            <ReadLink to={post.slug}>Read this Post &rarr;</ReadLink>
        </article>
    );
};

export default PostPreview;
