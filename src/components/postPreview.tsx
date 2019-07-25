import { Link } from 'gatsby';
import React from 'react';
import { css } from '@emotion/core';
import ReadLink from './readLink';
import Image from 'gatsby-image';
import { PostData } from '../hooks/use-posts';

interface PostPreviewProps {
    post: PostData;
}
const PostPreview = ({ post }: PostPreviewProps) => {
    return (
        <article
            css={css`
                border-bottom: 1px solid #ddd;
                display: flex;
                padding-bottom: 1rem;
                margin-top: 0;

                &:first-of-type {
                    margin-top: 1rem;
                }
            `}
        >
            <Link
                to={post.slug}
                css={css`
                    margin: 1rem 1rem 0 0;
                    width: 100px;
                `}
            >
                <Image
                    fluid={post.image}
                    css={css`
                        * {
                            margin: 0;
                        }
                    `}
                    alt={post.title}
                ></Image>
            </Link>
            <div>
                <h3>
                    <Link to={post.slug}>{post.title}</Link>
                </h3>
                <h3>{post.excerpt}</h3>
                <ReadLink to={post.slug}>Read this Post &rarr;</ReadLink>
            </div>
        </article>
    );
};

export default PostPreview;
