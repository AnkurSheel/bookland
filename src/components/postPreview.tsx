import { Link } from 'gatsby';
import React from 'react';
import { css } from '@emotion/core';
import ReadLink from './readLink';
import Image from 'gatsby-image';
import { PostData } from '../hooks/use-posts';

const styles = {
    article: css({
        display: 'flex',
        borderBottom: '1px solid #ddd',
        paddingBottom: '1rem1',
        marginTop: 0,
        '&:first-of-type': {
            marginTop: '1rem',
        },
    }),
    link: css({
        margin: '1rem 1rem 0 0',
        width: '100%',
    }),
    image: css({
        margin: 0,
    }),
};

interface PostPreviewProps {
    post: PostData;
}
const PostPreview = ({ post }: PostPreviewProps) => {
    return (
        <article css={styles.article}>
            <Link to={post.slug} css={styles.link}>
                <Image fluid={post.image} css={styles.image} alt={post.title}></Image>
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
