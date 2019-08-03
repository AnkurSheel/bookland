import { css } from '@emotion/core';
import { Link } from 'gatsby';
import { ReactNode } from 'react';

const styles = {
    link: css({
        display: 'inline-block',
        fontSize: '0.875rem',
    }),
};

interface IReadLinkProps {
    to: string;
    children: ReactNode;
}
const ReadLink = (props: IReadLinkProps) => (
    <Link css={styles.link} to={props.to}>
        {props.children}
    </Link>
);

export default ReadLink;
