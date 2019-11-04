import { css } from '@emotion/core';
import { Link } from 'gatsby';
import Image from 'gatsby-image';
import React from 'react';
import { PostData } from '../hooks/use-posts';
import ReadLink from './readLink';

const styles = {
    article: css({
        display: 'grid',
        alignItems: 'center',
        borderBottom: '1px solid #ddd',
        paddingBottom: '1rem',
        margin: '1rem',
        gridColumnGap: '1rem',
        gridTemplateColumns: 'minmax(150px,1fr) minmax(150px, 2fr)',
        justifyItems: 'center',
    }),
    link: css({
        width: '100%',
        textDecoration: 'none',
        maxWidth: '10rem',
    }),
    image: css({
        margin: 0,
        width: '100%',
    }),
    text: css({
        display: 'flex',
        flexDirection: 'column',
    }),
    title: css({
        textDecoration: 'none',
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
            <div css={styles.text}>
                <h3>
                    <Link css={styles.title} to={post.slug}>
                        {post.title}
                    </Link>
                </h3>
                <p>{post.excerpt}</p>
                <ReadLink to={post.slug}>Read this Post &rarr;</ReadLink>
            </div>
        </article>
    );
};

export default PostPreview;
